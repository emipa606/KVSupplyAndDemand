# [[KV] Supply and Demand (Continued)]()

![Image](https://i.imgur.com/buuPQel.png)

Update of CookieWookiee (original) and Kiame Vivacitys mod https://steamcommunity.com/sharedfiles/filedetails/?id=1511375007

![Image](https://i.imgur.com/pufA0kM.png)
	
![Image](https://i.imgur.com/Z4GOv8H.png)

Capitalism? On my Rimworld? It's more likely than you think!
If you are like me, you have played colonies to their bitter end, and had a few amazing successes where your pawns have amassed riches to the envy of the Rim. But what do you do with all of that money? Traders, when they visit, can barely afford to purchase your legendary golden statues. And that is being generous.

Wouldn't it make sense that traders visiting a rich colony will bring more goods to trade because... the colony is rich?

That's what Suppy and Demand does. Rather than creating new TraderKindDefs and manually giving them oodles of silver, human leather, and charge rifles, this mod takes your total colony wealth, scales it by game difficulty, then takes the natural logarithm of that to produce a scalar with diminishing returns that it then multiplies the stock of your trader by. If that all sounds hard to follow, here's the tl;dr: the mod scales trader stock based on colony wealth.


**Mathbrain Stuff**
If all you want is scaling things, you can tune out now. For the rest of you, this is the explaination of what I am doing and how I'm doing it.

I've wanted to implement a system that allows visiting traders to bring more stuff if you have more stuff to trade with them. It makes sense from an economic standpoint: your colonists have greater demand because they're rich, and traders are willing to supply more because they can get more. I was dissatisfied with XML patches because they failed to account for my addition to downloading new things, and definitely can't be scaled dynamically as you collect new things.

Wealth was the most obvious variable to choose to scale by, but it needed to be first compressed to keep traders from having two million chickens in their inventory, and it needed to grow at a slower rate than wealth, getting progressively slower over time to model diminishing returns. The natural log has both of these properties. To preserve the significance of difficulty, wealth is scaled by the price loss factor that the difficulty setting provides.

![Image](https://i.imgur.com/KCaTYO6.png)

This equation is the scaling formula used in this mod. Lambda represents total colony wealth. Phi represents the difficulty scalar. The minimum value this function can take on within the domain is 1.



Original mod done by Cookie Wookie. Cookie, if you want me to remove this mod i will.


Direct Download:
https://github.com/KiameV/rimworld-supplyanddemand/releases/download/1.3/SupplyAndDemand.zip

![Image](https://i.imgur.com/PwoNOj4.png)



-  See if the the error persists if you just have this mod and its requirements active.
-  If not, try adding your other mods until it happens again.
-  Post your error-log using [HugsLib](https://steamcommunity.com/workshop/filedetails/?id=818773962) or the standalone [Uploader](https://steamcommunity.com/sharedfiles/filedetails/?id=2873415404) and command Ctrl+F12
-  For best support, please use the Discord-channel for error-reporting.
-  Do not report errors by making a discussion-thread, I get no notification of that.
-  If you have the solution for a problem, please post it to the GitHub repository.
-  Use [RimSort](https://github.com/RimSort/RimSort/releases/latest) to sort your mods


