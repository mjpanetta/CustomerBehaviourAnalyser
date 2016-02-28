CustomerBehaviorAnalyser...

Desription:

Reads in customer bet data from a csv file (location and filename in app settings), aggregates bets to customers and marks suspicious bets with the risk level (Unusual, HighlyUnusual or Risky)
CSV files need to be in format Customer,Event,Participant,Stake,Win for settled bets or Customer,Event,Participant,Stake,To Win for unsettled bets. 
(Note: currently event and participant aren't used, however stake and win/towin still need to be in the 4th and 5th rows respectively if you were to remove them).
Will also mark the customer if they have won more then 60% of their bets.

Current Rules
All upcoming bets from customers that win at an unusual rate are marked as Risky
Bets where the stake is more than 10 times higher than the customers average are unsual
Bets where the stake is more than 30 times higher than the customers average are highly unusual
Bets where the amount to be won is $1000 or more are marked as unsual

How to run:

A simple console app, either start CustomerBehaviorAnalyser in Visual studio and right click on CustomerBehaviorAnalyser > Debug > Start new instance 
or run the exe directly in folder CustomerBehaviourAnalyser\CustomerBehaviourAnalyser\bin\Debug\CustomerBehaviorAnalyser.exe. Results will appear in C:\Source\CustomerBehaviourAnalyser\CustomerBehaviourAnalyser