# kubuswerk
GATDEV's Lightcube daemon, driver, and 'duino code.

##OpCode format

Opcodes are sent to the Arduino via USB. They control the lighting pattern of the cube: which lights are involved; which patterns is used; and for how many repetitions. Each opcode is 1 byte long in the following format:

```
RGPPNNNN
```
Where:
* ```R```: 1 to apply pattern to red; 0 to exclude.
* ```G```: 1 to apply pattern to green; 0 to exclude. *Note: turning on red AND green will make yellow!*
* ```PP```: Two bits used to indicate four lighting patterns (mnemonics highlighted):
  * ```00```: Turn ```OFF``` the selected lights
  * ```01```: Turn ```ON``` the selected lights (hold them steady, no blinks or anything like that)
  * ```10```: ```BLINK``` the selected lights
  * ```11```: ```PULSE``` the selected lights (i.e. a blink with a fade in/out from/to off)
* ```NNNN```: Four bits indicating number of repetitions for the above pattern. If this value is 0, it sets the cube state, i.e. the cube will hold this pattern forever. For the ```ON``` and ```OFF``` patterns, this is the number of seconds that the lights will hold their current state; for ```BLINK``` and ```PULSE```, this is the number of repetitions. After the number of reps have been completed, the cube reverts to its previous state.

*Examples*

```10010000```: Sets the red channel to be ```ON``` forever.

```10100011```: ```BLINK```s the red channel 3 times. If run immediately after the previous example, the light will blink three times and then go back to being solid red.

```01010000```: Sets the green channel to be ```ON``` forever. Note that this does NOT turn ```OFF``` red, so if this opcode was run immediately after the first example, then both red and green would be ```ON```, thus making the cube yellow forever.
