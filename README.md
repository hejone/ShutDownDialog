## Shutdown dialog

Very simple shutdown dialog to make sure user wants to shutdown the computer.

## But why?

This uses the Windows command to actually shut down the computer, not hibernate it.
This should prevent some writes to SSD. Also, makes sure user don't accidentally shut down
the computer using a shortcut to the command (*may have happened to the creator a few times* 😉)

## Command line arguments

This software has following command line arguments:
- `-t` or `--time` can be used to set the automatic executable timer time. The executed command can selected with `--default`.
  - The valid values are  the range \[1..180\] s. The default value is 10.
- `--default` can be used to select the default command to run. Currently only `shutdown` and `restart` can be chosen.
  - The valid values are  `shutdown` and `restart`. The default is `shutdown`.
