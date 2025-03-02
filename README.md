# AlphaPIG
AlphaPIG is a meta-technique designed to Prolong Interactive Gestures by leveraging real-time fatigue predictions. AlphaPIG assists designers in extending and improving XR interactions by enabling automated fatigue-based interventions. Through adjustment of intervention timing and intensity decay rate, designers can explore and control the trade-off between fatigue reduction and potential effects such as decreased body ownership. 

## Input Parameters:
1. fatigueThreshold: Fatigue threshold to activate external ergonomic interaction techniques.
2. decayRateAlpha: The decay rate of Alpha.
3. manipulationMax: The maximum intervention effect of the chosen interaction variable.
4. manipulationMin: The default value of the chosen interaction variable.

## Output:
1. AlphaPIGManipulationVariable: The AlphaPIG-modified interaction variable.

## Notes:
The configuration of the implemented fatigue model NICER can be found at https://github.com/ylii0411/NICER_Unity_API.

## Reference
Please refer to the following publication for more details:
[1] Yi Li, Florian Fischer, Tim Dwyer, Barrett Ens, Robert Crowther, Per Ola Kristensson, and Benjamin Tag. 2025. AlphaPIG: The Nicest Way to Prolong Interactive Gestures in Extended Reality. In CHI Conference on Human Factors in Computing Systems (CHI '25), April 26–May 01, 2025, Yokohama, Japan. ACM, New York, NY, USA, 14 pages. https://doi.org/10.1145/3706598.3714249
