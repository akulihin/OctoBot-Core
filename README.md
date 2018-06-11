# OctoBot-Core
**THE FILE IN PROGRESS EDITING**

This is a C# project using 3rd party API, for to interact with Discord users. 

Interesting commands, from programmer perspective:

*P.s. there is A LOT of commands and interactions, and even **a game** in progress!*

1)**Remind** AnyText **IN** 1d13h33m33s This command will remind you anything you want in the time you ask as a DM message.

How it works: It creates a struct with calculated time WhenToPost, and the text you wanted and save it to accounts file. Every 5 seconds app is checking all users have a reminder in account file, and if date now => WhenToPost, it will try to send the message, if success - delete it from the file. So this will work even if the application was restarted. **mute @user** works in the same way. You also can make this reminder for other people.

2)In *OctoBot-Core/OctoBot/Commands/ShadowCItyCOmmand/* I have two messages with multiple emojis under it. Both of them made for users to **get or delete a server role(tag) by pressing on particular emoji**, which gives access to closed rooms on the server or just a colour on your nickname. ( one mess for colours, another for rooms)

How it works: every time a user placing an emoji under any message, the application is triggered by an event - placed emoji, which takes 3 parameters user, message, emoji. if messageID == to a particular ID, it will check if you have the role assigned to this emoji, if yes - delete, no - add it.
