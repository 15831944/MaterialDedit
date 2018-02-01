using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using NestBridge;

// the nesting result.
public class NestResult 
{
    public NestResult()
    {
    }

	public SheetListEx GetNestResultByIndex(int iIndex)
    {
        SheetListEx nestResult;

	    m_mutex.WaitOne();
	    nestResult = (SheetListEx)m_nestResults[iIndex];
	    m_mutex.ReleaseMutex();

	    return nestResult;
    }

	public void AddNestResult(SheetListEx nestResult)
    {
        m_mutex.WaitOne();
	    m_nestResults.Add(nestResult);
	    m_mutex.ReleaseMutex();
    }

	public int GetNestResultCount()
    {
        int iCount = 0;

        m_mutex.WaitOne();
        iCount = m_nestResults.Count;
	    m_mutex.ReleaseMutex();

        return iCount;
    }

    public int GetElapsedTime()
    {
        int iElapsedTime = 0;

        m_mutex.WaitOne();
        iElapsedTime = m_iElapsedTime;
        m_mutex.ReleaseMutex();

        return iElapsedTime;
    }

    public void SetElapsedTime(int iElapsedTime)
    {
        m_mutex.WaitOne();
        m_iElapsedTime = iElapsedTime;
        m_mutex.ReleaseMutex();
    }

	public bool TaskFinished()
    {
        bool b;

        m_mutex.WaitOne();
        b = m_bTaskFinished;
        m_mutex.ReleaseMutex();

        return b;
    }

	public void TaskFinished(bool bTaskFinished)
    {
        m_mutex.WaitOne();
        m_bTaskFinished = bTaskFinished;
        m_mutex.ReleaseMutex();
    }

    // the mutex which will protect the progress data.
    private Mutex m_mutex = new Mutex();

    // the nesting results from the kernel.
    private ArrayList m_nestResults = new ArrayList();

    // whether the task is finished. means the kernel finished the nesting and the client got all results.
    private bool m_bTaskFinished = false;

    // elapsed time.
    int m_iElapsedTime;
}
