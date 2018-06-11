# OctoBot-Core
**THE FILE IN PROGRESS EDITING**

This is a C# project using 3rd party API, for to interact with Discord users. 

Interesting commands, from programmer perspective:

*P.s. there is A LOT of commands and interactions, and even **a game** in progress!*

1)**Remind** AnyText **IN** 1d13h33m33s This command will remind you anything you want in the time you ask as a DM message.
How it works: It creates a struct with calculated time WhenToPost, and the text you wanted and save it to accounts file. Every 5 seconds app is checking all users have a reminder in account file, and if date now => WhenToPost, it will try to send the message, if success - delete it from the file. So this will work even if the application was restarted. **mute @user** works in the same way. You also can make this reminder for other people.

2)
