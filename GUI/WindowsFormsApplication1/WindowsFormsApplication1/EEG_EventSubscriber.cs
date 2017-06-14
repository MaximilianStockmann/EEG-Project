using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class EEG_EventSubscriber
{
    private EEG_EventThrower _Thrower;

    public EEG_EventSubscriber()
    {
        _Thrower = new EEG_EventThrower();
    }

    public void AssignTrainingStarted(Action referencedBlock)
    {
        _Thrower.TrainingStarted += (sender, args) => { referencedBlock(); };
    }
    public void AssignTrainingSucceeded(Action referencedBlock)
    {
        _Thrower.TrainingSucceeded += (sender, args) => { referencedBlock(); };
    }
    public void AssignTrainingCompleted(Action referencedBlock)
    {
        _Thrower.TrainingCompleted += (sender, args) => { referencedBlock(); };
    }
    public void AssignTrainingRejected(Action referencedBlock)
    {
        _Thrower.TrainingRejected += (sender, args) => { referencedBlock(); };
    }
    public void AssignCommandNeutral(Action referencedBlock)
    {
        _Thrower.CommandNeutral += (sender, args) => { referencedBlock(); };
    }
    public void AssignCommandLeft(Action referencedBlock)
    {
        _Thrower.CommandLeft += (sender, args) => { referencedBlock(); };
    }
    public void AssignCommandRight(Action referencedBlock)
    {
        _Thrower.CommandRight += (sender, args) => { referencedBlock(); };
    }
    public void AssignCommandForward(Action referencedBlock)
    {
        _Thrower.CommandForward += (sender, args) => { referencedBlock(); };
    }
    public void AssignCommandBackward(Action referencedBlock)
    {
        _Thrower.CommandBackward += (sender, args) => { referencedBlock(); };
    }
    public void AssignDongleConnected(Action referencedBlock)
    {
        _Thrower.DongleConnected += (sender, args) => { referencedBlock(); };
    }
    public void AssignDongleDisconnected(Action referencedBlock)
    {
        _Thrower.DongleDisconnected += (sender, args) => { referencedBlock(); };
    }
    public void AssignEngineConnected(Action referencedBlock)
    {
        _Thrower.EngineConnected += (sender, args) => { referencedBlock(); };
    }
    public void AssignEngineDisconnected(Action referencedBlock)
    {
        _Thrower.EngineDisconnected += (sender, args) => { referencedBlock(); };
    }

}