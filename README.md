# CaptainSonarRadioOp
A little hacked-together tool to help the Radio Operator in the Captain Sonar boardgame. 

# Use
* Use the keyboard arrows to track the movement of the enemy sub. 
* Backspace will undo movements
* `D` will record a drone event 
* `S` will record a sonar event. Only 2 out of 3 fields should be filled. 
* `G` will record a silent running event. 
* `T` will record the result of a friendly torpedo (direct hit, indirect hit, miss)
* `I` will record the result of an enemy torpedo.

# Backlog of Improvements
* Serious refactoring needed for code cleanliness
* Paralellization of option tree pruning using Parallel library
* Adding other maps
* Resetting without restarting 
* Adding buttons for actions and make the shortcut keys obvious in the UI
* Silent Running pruning algorithm takes into account that Silent Running cannot transverse over own path
* Add enemy mine events (lay, detonate) 

