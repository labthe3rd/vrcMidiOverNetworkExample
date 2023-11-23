/*  Programmer:     labthe3rd
 *  Date:           11/22/23
 *  Desc:           Example demonstrating midi commands and manual udon sync method
 */
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using VRC.Udon.Common;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class MidiNetworkManualExample : UdonSharpBehaviour
{
    //Midi On/Off UI
    public Text midiOnOffChanneltext;
    public Text midiOnOffNumberText;
    public Text midiOnOffVelocityText;
    public Text midiOnTimeText;
    public Text midiOffTimeText;

    //Network On/Off Data
    public Text netOnOffChannelText;
    public Text netOnOffNumberText;
    public Text netOnOffVelocityText;
    public Text netOnReceivedText;
    public Text netOffReceivedText;

    //Midi Value Changed UI
    public Text midiChangeChannelText;
    public Text midiChangeNumberText;
    public Text midiChangeValueText;


    //Network Midi Value Changed UI
    public Text netChangeChannelText;
    public Text netChangeNumberText;
    public Text netChangeValueText;
    public Text netChangeReceiveText;

    //General UI
    public Text generalMessage;
    public Text generalChangeAttemptText;
    public Text generalChangeSentText;
    public Text generalChangeLostText;


    //Local testing bit so you can use multiple instances of VRChat
    public bool localTest;

    [UdonSynced] private int midiOnOffChannel;
    [UdonSynced] private int midiOnOffNumber;
    [UdonSynced] private int midiOnOffVelocity;
    [UdonSynced] private bool midiOnState;
    [UdonSynced] private bool midiOffState;
    [UdonSynced] private int midiChangeChannel;
    [UdonSynced] private int midiChangeNumber;
    [UdonSynced] private int midiChangeValue;

    private int lastMidiChangeChannel;
    private int lastMidiChangeNumber;

    private int valueAttemptCounter;
    private int valueSuccessCounter;
    private int valueLostCounter;

    private bool lastMidiOnState;
    private bool lastMidiOffState;


    public override void MidiNoteOn(int channel, int number, int velocity)
    {
        Debug.Log("Midi Note On Received: " + channel + number + velocity);
        
        //Set self to owner
        if (!Networking.IsOwner(this.gameObject) && !localTest)
        {
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        }

        //Confirm ownership
        if (Networking.IsOwner(this.gameObject))
        {
            Message("Midi Note On CH:" + channel + " Number:" + number + " Velocity:" + velocity);
            midiOnOffChannel = channel;
            midiOnOffNumber = number;
            midiOnOffVelocity = velocity;
            midiOnState = !midiOnState;

            //Send values to other players
            RequestSerialization();

            //Update UI
            midiOnOffChanneltext.text = midiOnOffChannel.ToString();
            midiOnOffNumberText.text = midiOnOffNumber.ToString();
            midiOnOffVelocityText.text = midiOnOffVelocity.ToString();
            midiOnTimeText.text =Networking.GetNetworkDateTime().ToString();
        }
    }

    public override void MidiNoteOff(int channel, int number, int velocity)
    {
        Debug.Log("Midi Note Off Received: " + channel + number + velocity);
        
        //Set self to owner
        if (!Networking.IsOwner(this.gameObject) && !localTest)
        {
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        }

        //Confirm ownership
        if (Networking.IsOwner(this.gameObject))
        {
            Message("Midi Note Off CH:" + channel + " Number:" + number + " Velocity:" + velocity);
            midiOnOffChannel = channel;
            midiOnOffNumber = number;
            midiOnOffVelocity = velocity;
            midiOffState = !midiOffState;

            //Update UI
            midiOnOffChanneltext.text = midiOnOffChannel.ToString();
            midiOnOffNumberText.text = midiOnOffNumber.ToString();
            midiOnOffVelocityText.text = midiOnOffVelocity.ToString();
            midiOffTimeText.text = Networking.GetNetworkDateTime().ToString();
        }
    }

    public override void MidiControlChange(int channel, int number, int value)
    {
        Debug.Log("Midi Change Received: " + channel + number + value);
       
        //Set self to owner
        if (!Networking.IsOwner(this.gameObject) && !localTest)
        {
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        }

        //Confirm ownership
        if (Networking.IsOwner(this.gameObject))
        {
            Message("Midi Control Change CH:" + channel + " Number:" + number + " Value:" + value);
            midiChangeChannel = channel;
            midiChangeNumber = number;
            midiChangeValue = value;

            RequestSerialization();

            //Update UI
            midiChangeChannelText.text = midiChangeChannel.ToString();
            midiChangeNumberText.text = midiChangeNumber.ToString();
            midiChangeValueText.text = midiChangeValue.ToString();


            lastMidiChangeChannel = midiChangeChannel;
            lastMidiChangeNumber = midiChangeNumber;
        }
    }

    public override void OnPreSerialization()
    {
        valueAttemptCounter++;
        generalChangeAttemptText.text = valueAttemptCounter.ToString();
    }

    public override void OnPostSerialization(SerializationResult result)
    {

            if (!result.success)
            {

                valueLostCounter++;
                generalChangeLostText.text = valueLostCounter.ToString();
            generalChangeSentText.text = valueSuccessCounter.ToString();

        }
        else
        {
            valueSuccessCounter++;
            generalChangeSentText.text = valueSuccessCounter.ToString();
            generalChangeLostText.text = valueLostCounter.ToString();
        }
        
    }

    public override void OnDeserialization()
    {
        Debug.Log("New data received...");
        netOnOffChannelText.text = midiOnOffChannel.ToString();
        netOnOffNumberText.text = midiOnOffNumber.ToString();
        netOnOffVelocityText.text = midiOnOffVelocity.ToString();
        if (midiOnState != lastMidiOnState)
        {
            netOnReceivedText.text = Networking.GetNetworkDateTime().ToString();
            lastMidiOnState = midiOnState;
        }
        if(midiOffState != lastMidiOffState)
        {
            netOffReceivedText.text = Networking.GetNetworkDateTime().ToString();
            lastMidiOffState = midiOffState;
        }

        netChangeChannelText.text = midiChangeChannel.ToString();
        netChangeNumberText.text = midiChangeNumber.ToString();
        netChangeValueText.text = midiChangeValue.ToString();
        netChangeReceiveText.text = Networking.GetNetworkDateTime().ToString();

    }

    public void Message(string message)
    {
        generalMessage.text = message;
    }

    public override bool OnOwnershipRequest(VRCPlayerApi requestingPlayer, VRCPlayerApi requestedOwner)
    {
        return true;
    }
}
