using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class EEG_EventThrower
{
    public delegate void EventHandler(object sender, EventArgs args);

    public event EventHandler TrainingStarted = delegate { };
    public event EventHandler TrainingSucceeded = delegate { };
    public event EventHandler TrainingCompleted = delegate { };
    public event EventHandler TrainingRejected = delegate { };
    public event EventHandler CommandNeutral = delegate { };
    public event EventHandler CommandLeft = delegate { };
    public event EventHandler CommandRight = delegate { };
    public event EventHandler CommandForward = delegate { };
    public event EventHandler CommandBackward = delegate { };
    public event EventHandler DongleConnected = delegate { };
    public event EventHandler DongleDisconnected = delegate { };
    public event EventHandler EngineConnected = delegate { };
    public event EventHandler EngineDisconnected = delegate { };

    public void TriggerEvent(string name)
    {
        switch (name)
        {
            case "TrainingStarted": TrainingStarted(this, new EventArgs()); break;
            case "TrainingSucceeded": TrainingSucceeded(this, new EventArgs()); break;
            case "TrainingCompleted": TrainingCompleted(this, new EventArgs()); break;
            case "TrainingRejected": TrainingRejected(this, new EventArgs()); break;
            case "CommandNeutral": CommandNeutral(this, new EventArgs()); break;
            case "CommandLeft": CommandLeft(this, new EventArgs()); break;
            case "CommandRight": CommandRight(this, new EventArgs()); break;
            case "CommandForward": CommandForward(this, new EventArgs()); break;
            case "CommandBackward": CommandBackward(this, new EventArgs()); break;
            case "DongleConnected": DongleConnected(this, new EventArgs()); break;
            case "DongleDisconnected": DongleDisconnected(this, new EventArgs()); break;
            case "EngineConnected": EngineConnected(this, new EventArgs()); break;
            case "EngineDisconnected": EngineDisconnected(this, new EventArgs()); break;
        }
    }
}
