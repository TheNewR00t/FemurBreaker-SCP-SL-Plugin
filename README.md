# Tutorial how to install
Download the plugin and its dependencies and put the DLLs in the EXILED plugins folder. Once you have placed all the files, start the server. A folder called 'audio' will be created; put the OGG file inside that folder. Remember to download the dependencies and MapEditorReborn to be able to load the plugin. Have fun!
# Create your own table
To create your own table in Unity with MapEditorReborn, you need to create a button called 'Button106'.
# Translation
The translations file is the file where you can change the language
```
# Onfailure
on_failure: '¡The recontainment of scp 106 has failed! Find another person for recontainment!'
# OnDeath
on_death: 'SCP-106 has been successfully recontained!'
# OnRecontainmentRepeat
on_recontainment_repeat: 'The old bastard has been recontained!'
# OnRequirements
on_requirements: 'Someone still needs to sacrifice themselves!'
# OnRequerimentsComplete
on_requeriments_complete: 'One has already sacrificed themselves!'
# OnSacrificeDeathReason
on_sacrifice_death_reason: 'You''re a hero!'
# Cassie sound when 106 holds back (You will need to have "UseCassie" disabled)
cassie: 'SCP 1 0 6 HAS BEEN SUCCEFULY RECONTAINED BY FEMUR BREAKER'
```
# Configuration file
If you want to change the language, you can do it in the configuration file. If you also want to change the accuracy percentage, it's there as well. Additionally, if you want to disable the audio bot, that's also there.
```
is_enabled: true
debug: false
# How many percent do you want the femurbreaker to get right?
porcent: 25
# OnRecontainmentDeath
on_recontainment_death: 'recontained!'
# Bot name
on_name_bot: 'C.A.S.S.I.E'
# If you want the femurbreaker sound activate this
npc: true
# seconds of audio
seconds: 33
# Activate this if you want a cassie to say when someone sacrifices
cassie_with_sacrificie: true
# UseCassie
sound_or_notsound: true
# Use generators
use_generators: false
# How many generators so that it can be activated
generators: 0
# How many generators so that it can be activated
text_generators: 'Generators: '
```
# Spawn the schematic automatically
To make the table schematic spawn automatically, you need to spawn the table with "mp cr 106recontainment" and then move it with "mp pos grab". To save it, use "mp save Femurbreaker". Then, go to the settings of "MapEditorReborn" and where it says "Load_map_on_event:", where it says "on_round_started: []", put it in "on_round_started:" as shown in the picture below.

![image](https://github.com/TheNewR00t/FemurBreaker-SCP-SL-Plugin/assets/126024362/3a94220a-e20b-4b51-8729-748ebcff1e98)
<img width="1920" height="1080" alt="{3D2911FE-5EA3-4AB3-8982-07A8E6F94C86}" src="https://github.com/user-attachments/assets/8ee94e60-c698-4e4c-8e11-996f54bded43" />
