# Unity Animation Rigging: Stepped Constraint
This is a custom constraint built with Unity's Animation Rigging Package by allowing control of the framerate of an animation. The idea was to emulate frame-by-frame animation that you see in traditional 2d animations. 

example- https://www.youtube.com/watch?v=63TGpArrnqQ&feature=youtu.be

The core overall idea is that you have 2 rigs: one that has the original animation applied, and one that you will apply the steppedConstraint to. By applying the constraint to the whole hierarchy, you can achieve this framerate effect. 
![alt text](https://github.com/RetroSpecter/UnitySteppedConstraint/blob/master/ReadMeImages/sample.PNG)


## SteppedConstraint.cs
![alt text](https://github.com/RetroSpecter/UnitySteppedConstraint/blob/master/ReadMeImages/SteppedConstraint.PNG)
  
Source - Transform you want to apply the constraint to
Target - bone with original motion you want to track
FPS - the rate at which the source will update its position and rotation to its target
Maintain Offset - if you want the source to maintain it's offset from the traget bone. 

## SteppedConstraintController.cs
![alt text](https://github.com/RetroSpecter/UnitySteppedConstraint/blob/master/ReadMeImages/SteppedConstraintController.PNG)
  
This can be applied to an animation rig to control all the SteppedConstraints in the below hierarchy. 



## Stepped Constraint Chain Editor
![alt text](https://github.com/RetroSpecter/UnitySteppedConstraint/blob/master/ReadMeImages/SteppedConstraintChainEditor.PNG)
  
This is a tool that allows you to create a stepped constraint rig based off the hierarchy of the source rig and the original target rig that has the animations applied to it. 

Source Rig - Rig that we will apply the effect to. This would be what the character mesh is skinned to.
Target Rig - rig that we will pull the position from and has the original animation. 

Once you select those, in the hierarchy select the GameObject you want to put the new rig on, and hit the button. 

## Credits

Jammo - https://github.com/mixandjam/Jammo-Character
