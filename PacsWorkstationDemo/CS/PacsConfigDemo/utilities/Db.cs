// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using Leadtools.Dicom;
using System.Data.SqlServerCe;
using System.IO;
using Leadtools.Dicom.AddIn.Interfaces;

namespace PACSConfigDemo
{
    public static class DB    
    {
        private static GetValueDelegate GetDate = delegate(string data)
                                                    {
                                                        try
                                                        {
                                                            return DateTime.Parse(data);
                                                        }
                                                        catch
                                                        {
                                                            return null;
                                                        }
                                                    };

        private const string PatientInsert = "INSERT INTO Patients(PatientId,Name,BirthDate,Sex,EthnicGroup,Comments,AETitle) " +
                                             "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"; 
        
        public static DicomCommandStatusType Insert(DateTime receive,string ConnectionString, string ImageDirectory,string AETitle,DicomDataSet dataset)
        {
            string sop = dataset.GetValue<string>(DicomTag.SOPInstanceUID, string.Empty);
            DicomCommandStatusType status = DicomCommandStatusType.Success;

            if (!string.IsNullOrEmpty(sop))
            {
                //string filename = info.ImageDirectory + sop + ".dcm";
                string pid = string.Empty;
                string studyInstance = string.Empty;
                string seriesInstance = string.Empty;
                string sopInstance = string.Empty;

                pid = AddPatient(ConnectionString, AETitle, dataset);
                studyInstance = AddStudy(receive, pid, ConnectionString, AETitle, dataset);
                seriesInstance = AddSeries(receive, studyInstance, ConnectionString, AETitle, dataset);
                status = AddImage(receive, sop,studyInstance, seriesInstance, ConnectionString, AETitle, dataset, ImageDirectory + pid + @"\");
            }

            return status;
        }

        /// <summary>
        /// Adds the patient.
        /// </summary>
        /// <param name="ConnectionString">The connection string.</param>
        /// <param name="AETitle">The AE title.</param>
        /// <param name="dataset">The dataset.</param>
        /// <returns></returns>
        private static string AddPatient(string ConnectionString, string AETitle, DicomDataSet dataset)
        {
            string pid = dataset.GetValue<string>(DicomTag.PatientID, string.Empty);

            if (string.IsNullOrEmpty(pid))
                throw new ArgumentException("Missing dicom tag", "Patient ID");

            if (!RecordExists(ConnectionString, "Patients", "PatientID = '" + pid + "'"))
            {
                DateTime? bd = dataset.GetValue<DateTime?>(DicomTag.PatientBirthDate, null, GetDate);
                DateTime? bt = dataset.GetValue<DateTime?>(DicomTag.PatientBirthTime, null, GetDate);
                string sql = string.Format(PatientInsert, pid,
                                           dataset.GetValue<string>(DicomTag.PatientName, string.Empty),
                                           GetDateString(bd,bt),
                                           dataset.GetValue<string>(DicomTag.PatientSex, string.Empty),
                                           dataset.GetValue<string>(DicomTag.EthnicGroup, string.Empty),
                                           dataset.GetValue<string>(DicomTag.PatientComments, string.Empty),
                                           AETitle);

                SqlCeHelper.ExecuteNonQuery(ConnectionString, sql);
            }

            return pid;
        }

        /// <summary>
        /// Adds the study.
        /// </summary>
        /// <param name="receive">The receive.</param>
        /// <param name="PatientId">The patient id.</param>
        /// <param name="ConnectionString">The connection string.</param>
        /// <param name="AETitle">The AE title.</param>
        /// <param name="dataset">The dataset.</param>
        /// <returns></returns>
        private static string AddStudy(DateTime receive, string PatientId, string ConnectionString, string AETitle, DicomDataSet dataset)
        {
            string studyInstance = dataset.GetValue<string>(DicomTag.StudyInstanceUID, string.Empty);

            if (string.IsNullOrEmpty(studyInstance))
                throw new ArgumentException("Missing dicom tag", "Study Instance UID");
                                  
            if (!RecordExists(ConnectionString, "Studies", "StudyInstanceUID = '" + studyInstance + "'"))
            {
                DateTime? sd = dataset.GetValue<DateTime?>(DicomTag.StudyDate, null, GetDate);
                DateTime? st = dataset.GetValue<DateTime?>(DicomTag.StudyTime, null, GetDate);                
                SqlCeResultSet rs = SqlCeHelper.ExecuteResultSet(ConnectionString, "Studies");
                SqlCeUpdatableRecord study = rs.CreateRecord();

                study.SetValue(0, studyInstance);
                study.SetValue(1, GetDateString(sd,st));
                study.SetValue(2, dataset.GetValue<string>(DicomTag.AccessionNumber,string.Empty));
                study.SetValue(3, dataset.GetValue<string>(DicomTag.StudyID, string.Empty));
                study.SetValue(4, dataset.GetValue<string>(DicomTag.ReferringPhysicianName, string.Empty));
                study.SetValue(5, dataset.GetValue<string>(DicomTag.StudyDescription, string.Empty));
                study.SetValue(6, dataset.GetValue<string>(DicomTag.AdmittingDiagnosesDescription, string.Empty));

                string age = dataset.GetValue<string>(DicomTag.PatientAge, string.Empty);

                if (age != string.Empty && age.Length > 0)
                    age = age.Substring(0, 4);

                study.SetValue(7, age);
                study.SetValue(8, dataset.GetValue<double>(DicomTag.PatientSize, 0));
                study.SetValue(9, dataset.GetValue<double>(DicomTag.PatientWeight, 0));
                study.SetValue(10, dataset.GetValue<string>(DicomTag.Occupation, string.Empty));
                study.SetValue(11, dataset.GetValue<string>(DicomTag.AdditionalPatientHistory, string.Empty));
                study.SetValue(12, dataset.GetValue<string>(DicomTag.InterpretationAuthor, string.Empty));
                study.SetValue(13, PatientId);
                study.SetValue(14, GetDateString(receive, receive));
                study.SetValue(15, AETitle);

                rs.Insert(study);
                rs.Close();                
            }

            return studyInstance;
        }

        /// <summary>
        /// Adds the series.
        /// </summary>
        /// <param name="receive">The receive.</param>
        /// <param name="StudyInstanceUid">The study instance uid.</param>
        /// <param name="ConnectionString">The connection string.</param>
        /// <param name="AETitle">The AE title.</param>
        /// <param name="dataset">The dataset.</param>
        /// <returns></returns>
        private static string AddSeries(DateTime receive, string StudyInstanceUid, string ConnectionString, string AETitle, DicomDataSet dataset)
        {
            string seriesInstance = dataset.GetValue<string>(DicomTag.SeriesInstanceUID, string.Empty);

            if (string.IsNullOrEmpty(seriesInstance))
                throw new ArgumentException("Missing dicom tag", "Series Instance UID");

            if (!RecordExists(ConnectionString, "Series", "SeriesInstanceUID = '" + seriesInstance + "'"))
            {
                DateTime? sd = dataset.GetValue<DateTime?>(DicomTag.SeriesDate, null, GetDate);
                DateTime? st = dataset.GetValue<DateTime?>(DicomTag.SeriesTime, null, GetDate);
                SqlCeResultSet rs = SqlCeHelper.ExecuteResultSet(ConnectionString, "Series");
                SqlCeUpdatableRecord series = rs.CreateRecord();

                series.SetValue(0, seriesInstance);
                series.SetValue(1, dataset.GetValue<string>(DicomTag.Modality, string.Empty));
                series.SetValue(2, dataset.GetValue<string>(DicomTag.SeriesNumber, string.Empty));

                string seriesDate = GetDateString(sd, st);

                if (seriesDate.Length > 0)
                    series.SetValue(3, seriesDate);

                series.SetValue(4, dataset.GetValue<string>(DicomTag.SeriesDescription, string.Empty));
                series.SetValue(5, dataset.GetValue<string>(DicomTag.InstitutionName, string.Empty));
                series.SetValue(6, GetDateString(receive, receive));
                series.SetValue(7, AETitle);
                series.SetValue(8, StudyInstanceUid);

                rs.Insert(series);
                rs.Close();
            }

            return seriesInstance;
        }

        private static DicomCommandStatusType AddImage(DateTime receive, string sopInstance, string StudyInstanceUid, string SeriesInstanceUid, 
                                       string ConnectionString, string AETitle, DicomDataSet dataset,string ImageDirectory)
        {             
            if (string.IsNullOrEmpty(sopInstance))
                throw new ArgumentException("Missing dicom tag", "SOP Instance UID");

            if (!RecordExists(ConnectionString, "Images", "SOPInstanceUID = '" + sopInstance + "'"))
            {
                string fileName = ImageDirectory + sopInstance + ".dcm";
                SqlCeResultSet rs = SqlCeHelper.ExecuteResultSet(ConnectionString, "Images");
                SqlCeUpdatableRecord image = rs.CreateRecord();

                image.SetValue(0, sopInstance);
                image.SetValue(1, SeriesInstanceUid);
                image.SetValue(2, StudyInstanceUid);
                if (HasValue(dataset, DicomTag.InstanceNumber))
                    image.SetValue(3, dataset.GetValue<int>(DicomTag.InstanceNumber, 0));
                image.SetValue(4, fileName);
                image.SetValue(5, dataset.GetValue<string>(DicomTag.TransferSyntaxUID, DicomUidType.ImplicitVRLittleEndian));
                image.SetValue(6, dataset.GetValue<string>(DicomTag.SOPClassUID, string.Empty));
                image.SetValue(7, dataset.GetValue<string>(DicomTag.StationName, string.Empty));
                image.SetValue(8, GetDateString(DateTime.Now, DateTime.Now));
                image.SetValue(9, AETitle);

                rs.Insert(image);
                rs.Close();

                //
                // store the file
                //
                if (!Directory.Exists(ImageDirectory))
                {
                    Directory.CreateDirectory(ImageDirectory);
                }

                dataset.Save(fileName, DicomDataSetSaveFlags.None);
            }
            else
                return DicomCommandStatusType.DuplicateInstance;

            return DicomCommandStatusType.Success;
        }

        /// <summary>
        /// Check to see if the record already exists.
        /// </summary>
        /// <param name="ConnectionString">The connection string.</param>
        /// <param name="table">The table.</param>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public static bool RecordExists(string ConnectionString,string table,string where)
        {
            string sql = "SELECT Count(*) FROM " + table + " WHERE " + where;
            object o = SqlCeHelper.ExecuteScalar(ConnectionString, sql);

            return o != null && Convert.ToInt32(o) != 0;
        }

        private static bool HasValue(DicomDataSet ds, long tag)
        {
            DicomElement e = ds.FindFirstElement(null, tag, false);

            return e != null && e.Length > 0;
        }

        /// <summary>
        /// Gets the SQL CE formatted date string.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static string GetDateString(DateTime? date,DateTime? time)
        {
            string combined = string.Empty;

            if(date!=null)
                combined = date.Value.ToString("yyyy-M-d") + " ";

            if (time != null)
                combined += time.Value.ToString("hh:mm:ss");

            combined = combined.Trim();

            return combined;
        }       
    }
}
