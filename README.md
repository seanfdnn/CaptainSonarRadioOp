# CaptainSonarRadioOp
A little hacked-together tool to help the Radio Operator in the Captain Sonar boardgame. Whether this is cheating or not - we'll leave that up to your morals and house rules. 

# Disclaimer
The creators of this software are in way associated with the publishers of the (awesome) board game. Although we do encourage people to buy the game.  
https://www.asmodee.us/en/games/captain-sonar/

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

# License
View the LICENSE file for use. 
