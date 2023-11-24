# vrcMidiOverNetworkExample

Example Unity Project and package that demonstrates how to sync midi data across a world's network.

See the world in [VRChat](https://vrchat.com/home/world/wrld_d8857e5b-b5df-4906-ab1c-0982e2884710)

![Picture Of The World](/Pictures/WorldPicture1.png)

## Testing Features

- Check Latency
- See live input from midi controller

## Quick Explanation

To allow midi to communicate to an Udon Script there is a few simple steps.

1. Add the VRC Midi Listener to a game object.
2. Reference the gameobject that hass the Udon Script that will be listening to the midi inputs.
   ![Picture Of Midi Listener](/Pictures/MidiListener.png)

## Useful Midi Software & Links

The midi software listed below can be useful and is all open source.

- [Loop Midi](https://www.tobias-erichsen.de/software/loopmidi.html) - Create virtual midi ports.
- [Midi-OX](http://www.midiox.com/) - Split midi signals, output different signals, all sorts of fun stuff you can use this for.
- [VMPK](https://vmpk.sourceforge.io/) - Virtual midi piano. I have found that you can edit the layout and create a custom controller. Great for doing things like changing lights and controlling parts of the world with a virtual midi controller.
- [VRChat Midi Documentation](https://creators.vrchat.com/worlds/udon/midi/) - Official Documentation.
- [Udon-MIDI-Web-Helper](https://github.com/DarthShader/Udon-MIDI-Web-Helper) - Uses midi to connect worlds to a web server. I have not personally tried this, but it looks cool.

# Donations Welcome!

If you found this repository useful and want to donate, it is very appreciated! I love creating content and sharing info from VRChat, every donation helps motivate me to create more content!

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?business=QXEYT9DHDXAUC&no_recurring=0&item_name=Help+inspire+me+to+continue+creating+new+VRChat+Prefabs+and+other+software%21&currency_code=USD)
