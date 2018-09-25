using Castle.DynamicProxy;
using NLog;
using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace GamesProject.Logic.Logging
{
    public class NLogInterceptor : IAsyncInterceptor
    {
        public void InterceptAsynchronous(IInvocation invocation)
        {
            string targetTypeName = invocation.TargetType.FullName;
            var logger = LogManager.GetLogger(targetTypeName);
            try
            {
                CompareAndLogArguments(invocation, targetTypeName, logger);
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw;
            }
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            string targetTypeName = invocation.TargetType.FullName;
            var logger = LogManager.GetLogger(targetTypeName);
            try
            {
                string invocationDesc = CompareAndLogArguments(invocation, targetTypeName, logger);

                ((Task<TResult>)invocation.ReturnValue)
                    .ContinueWith(task =>
                    {
                        if (task.Status == TaskStatus.RanToCompletion)
                        {
                            if (logger.IsDebugEnabled)
                            {
                                StringBuilder sbResult = new StringBuilder();
                                AppendObject(sbResult, task.Result);
                                logger.Debug("Result of " + invocationDesc + " is: " + sbResult);
                            }
                        }
                    });

            }
            catch (Exception e)
            {
                logger.Error(e);
                throw;
            }
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            string targetTypeName = invocation.TargetType.FullName;
            var logger = LogManager.GetLogger(targetTypeName);
            try
            {
                string invocationDesc = CompareAndLogArguments(invocation, targetTypeName, logger);

                if (logger.IsDebugEnabled)
                {
                    StringBuilder sbResult = new StringBuilder();
                    AppendObject(sbResult, invocation.ReturnValue);
                    logger.Debug("Result of " + invocationDesc + " is: " + sbResult);
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw;
            }
        }

        private static void AppendObject(StringBuilder sb, object obj)
        {
            if (obj is null)
            {
                sb.Append("NULL");
            }
            else if (obj is IEnumerable enumerableArgument)
            {
                sb.Append("[");
                IEnumerator enumerator = enumerableArgument.GetEnumerator();
                bool isFirstObject = true;
                while (enumerator.MoveNext())
                {
                    if (!isFirstObject)
                    {
                        sb.Append(", ");
                    }

                    AppendObject(sb, enumerator.Current);

                    if (isFirstObject)
                    {
                        isFirstObject = false;
                    }
                }

                sb.Append("]");
            }
            else
            {
                Type objType = obj.GetType();
                if (objType.IsPrimitive || obj is DateTime)
                {
                    sb.Append(obj);
                }
                else
                {
                    sb.Append("{");
                    sb.Append(obj);
                    sb.Append("}");
                }
            }
        }

        private static string CompareAndLogArguments(IInvocation invocation, string targetTypeName, Logger logger)
        {
            StringBuilder sb;
            if (logger.IsDebugEnabled)
            {
                sb = new StringBuilder(targetTypeName)
                    .Append(".")
                    .Append(invocation.Method)
                    .Append("(");
                for (int i = 0; i < invocation.Arguments.Length; i++)
                {
                    object argument = invocation.Arguments[i];
                    if (i > 0)
                    {
                        sb.Append(", ");
                    }

                    AppendObject(sb, argument);
                }

                sb.Append(")");
                logger.Debug(sb);
            }
            else
            {
                sb = new StringBuilder();
            }

            invocation.Proceed();
            return sb.ToString();
        }
    }
}
