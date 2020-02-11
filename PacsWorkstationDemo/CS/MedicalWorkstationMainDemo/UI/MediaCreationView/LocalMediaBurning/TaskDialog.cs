// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
#if FOR_DOTNET4
using System.Threading.Tasks;
#else
using System.Threading;
#endif

namespace Leadtools.Demos.Workstation
{
   public partial class TaskDialog : Form, ITaskDialog
   {
      public ITaskDialogPresenter Presenter { get; set; }

      public TaskDialog()
      {
         InitializeComponent();
         Presenter = new TaskDialogPresenter();
         Presenter.Initialize(this);
      }
      private void TaskDialog_Load(object sender, EventArgs e)
      {
         Presenter.OnViewLoad();
      }
      private void TaskDialog_Shown(object sender, EventArgs e)
      {
         Presenter.OnViewShown();
      }
      public void UpdateTaskName(string name)
      {
         label1.Text = name;
         label1.AutoSize = true;
      }      
      public void ShowView(IWin32Window parent)
      {
         base.ShowDialog(parent);
      }
      public void CloseView()
      {
         base.DialogResult = DialogResult.OK;
         base.Close();
      }
      
      delegate void CloseViewHandler();
      public void AsynchCloseView()
      {
         var closeViewHandler = new CloseViewHandler(CloseView);
         base.Invoke(closeViewHandler);
      }     
   }

   public interface ILTTask
   {
      void Run();
      void SetCallback(Action callback);
      bool IsCompleted();
      bool IsCompletedWithErrors();
      Exception GetError();
   }
   
   public interface ILTTask<R> : ILTTask
   {
      void SetTask(Func<R> function);
      R GetResult();
   }

#if FOR_DOTNET4
   public class LTTask<R> : ILTTask<R>
   {
      Task<R> _task = null;
      Func<R> _function = null;
      Action _callback = null;

      public void SetTask(Func<R> function) { _function = function; }
      public void SetCallback(Action callback) { _callback = callback; }
      public bool IsCompleted() { if (null == _task) return false; return _task.IsCompleted || _task.IsFaulted || _task.IsCanceled; }
      public bool IsCompletedWithErrors() { if (null == _task) return false; return _task.IsFaulted || _task.IsCanceled; }
      public R GetResult() { if (null == _task) return default(R); return _task.Result; }
      public Exception GetError() { if (null == _task) return null; return _task.Exception; }

      public void Wait()
      {
         _task.Wait();
      }

      public void Run()
      {
         _task = Task<R>.Factory.StartNew(_function);
         _task.ContinueWith((task => { _callback(); }));
      }
   }
#else
   //NET2 impl
   public delegate TResult Func<out TResult>();
   public delegate void Action();

   public class LTTask<R> : ILTTask<R>
   {
      R _result = default(R);
      Exception _e = null;
      bool _completed = false;
      object _sync = new object();
      Func<R> _function = null;
      Action _callback = null;

      public void SetTask(Func<R> function) { _function = function; }
      public void SetCallback(Action callback) { _callback = callback; }
      public bool IsCompleted() { lock (_sync) { return _completed; } }
      public bool IsCompletedWithErrors() { lock (_sync) { return _e != null; } }
      public R GetResult() { lock (_sync) { return _result; } }
      public Exception GetError() { lock (_sync) { return _e; } }

      public void Wait()
      {
         Func<R> dlgt = delegate
         {
            lock (_sync)
            {
               if (!_completed)
               {
                  Monitor.Wait(_sync);
               }
               return _result;
            }
         };
         dlgt();
      }

      public void Run()
      {
         IAsyncResult asyncResult = _function.BeginInvoke(
                 iAsyncResult =>
                 {
                    lock (_sync)
                    {
                       _result = _function.EndInvoke(iAsyncResult);
                       _completed = true;
                       Monitor.Pulse(_sync);
                       _callback();
                    }
                 }, null);
      }
   }
#endif
   public class TaskDialogFactory
   {
      public static TaskDialog Create(ILTTask model, string taskName)
      {
         var dlg = new TaskDialog();
         dlg.Presenter.BindTo(model, taskName);
         return dlg;
      }
   }

   public interface ITaskDialogPresenter
   {
      void Initialize(ITaskDialog view);
      void BindTo(ILTTask model, string taskName);

      void OnViewLoad();
      void OnViewShown();
   }

   public class TaskDialogPresenter : ITaskDialogPresenter
   {
      private string TaskName { get; set; }
      private ILTTask Model { get; set; }
      private ITaskDialog View { get; set; }
      object _synch = new object();
      
      public void Initialize(ITaskDialog view)
      {
         View = view;
      }
      public void BindTo(ILTTask model, string taskName)
      {
         lock (_synch)
         {
            Model = model;
            TaskName = taskName;
            UpdateModel();
            UpdateView();
            CheckTask();
         }
      }

      public void UpdateModel()
      {
         lock (_synch)
         {
            if (null == Model)
               return;

            Model.SetCallback(ModelTaskCompleted);
         }
      }
      public void ExecModel()
      {
         lock (_synch)
         {
            if (null == Model)
               return;
            Model.Run();
         }
      }
      public void UpdateView()
      {
         lock (_synch)
         {
            if (null == View)
               return;
            View.UpdateTaskName(TaskName);
         }
      }

      void CheckTask()
      {
         lock (_synch)
         {
            if (Model != null)
            {
               if (Model.IsCompleted())
               {
                  View.AsynchCloseView();
               }
            }
            else
            {
               View.CloseView();
            }
         }
      }
      
      public void ModelTaskCompleted()
      {
         //warning: call is from another thread
         CheckTask();
      }

      public void OnViewLoad()
      {
         UpdateView();
      }

      public void OnViewShown()
      {
         ExecModel();
         CheckTask();
      }
   }

   public interface ITaskDialog
   {
      ITaskDialogPresenter Presenter { get; set; }
      void UpdateTaskName(string name);
      void ShowView(IWin32Window parent);
      void CloseView();
      void AsynchCloseView();
   }
}
