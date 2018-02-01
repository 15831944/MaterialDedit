using System;
using System.Threading;

using NestBridge;

public class NestRstWatcher 
{
    public NestRstWatcher(NestProcessorEx nestProcessor, NestResult nestResult, int iNestingTime)
    {
        m_nestProcessor = nestProcessor;
        m_nestResult = nestResult;
        m_iNestingTime = iNestingTime;
        m_iStartTickCount = System.Environment.TickCount;
    }

    public void Run()
    {
        while (true)
        {
            Thread.Sleep(500);

            // get the nesting result.
            if (m_nestProcessor.BetterResultExist())
            {
                SheetListEx sheetList = m_nestProcessor.GetNestResult();
                m_nestResult.AddNestResult(sheetList);

                // write the log.
                m_nestProcessor.WriteLog("the watcher thread got a result from the kernel.");
            }

            // if the nesting is stopped and no more better result, quit the watcher.
            if (m_nestProcessor.IsStopped() && !m_nestProcessor.BetterResultExist())
            {
                // write the log.
                m_nestProcessor.WriteLog("the watcher thread quited.");

                m_nestResult.TaskFinished(true);
                break;
            }

            // whether the time is out.
            int iElapsedTime = (System.Environment.TickCount - m_iStartTickCount) / 1000;
            m_nestResult.SetElapsedTime(iElapsedTime);
            if (iElapsedTime > m_iNestingTime)
            {
                // stop the nesting.
                m_nestProcessor.StopNest();

                // write the log.
                m_nestProcessor.WriteLog("the watcher thread sent the stop command to the kernel.");
            }
        }
    }

    // the nesting processor.
    NestProcessorEx m_nestProcessor;

    // the nesting result.
    NestResult m_nestResult;

    // the start tick count.
    int m_iStartTickCount;

    // the allowed nesting time(s).
    int m_iNestingTime;
}
