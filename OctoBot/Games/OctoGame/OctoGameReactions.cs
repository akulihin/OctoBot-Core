using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Games.OctoGame.GameSpells;
using OctoBot.Games.OctoGame.GameUsers;

namespace OctoBot.Games.OctoGame
{
    internal static class OctoGameReaction
    {


        public static async Task ReactionAddedForOctoGameAsync(Cacheable<IUserMessage, ulong> cash,
            ISocketMessageChannel channel, SocketReaction reaction)
        {

            for (var i = 0; i < Global.OctopusGameMessIdList.Count ; i++)
            {
               
                    if (reaction.MessageId == Global.OctopusGameMessIdList[i].OctoGameMessIdToTrack &&  reaction.UserId == Global.OctopusGameMessIdList[i].OctoGameUserIdToTrack)
                    {
                       
                        
                        var globalAccount = Global.Client.GetUser(reaction.UserId);
                        var account = GameUserAccounts.GetAccount(globalAccount);

                        if (reaction.Emote.Name == "🐙")
                        {
                            
                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                    Global.OctopusGameMessIdList[i].SocketMsg);
                            

                        }
                        else if (reaction.Emote.Name == "⬅") 
                        {
                           

                                await OctoGameUpdateMess.FighhtReaction.SkillPageLeft(reaction,
                                    Global.OctopusGameMessIdList[i].SocketMsg);
                            

                        }
                        else if (reaction.Emote.Name == "➡") 
                        {
                          
                          
                            await OctoGameUpdateMess.FighhtReaction.SkillPageRight(reaction,
                                Global.OctopusGameMessIdList[i].SocketMsg);
                           
                        }
                        else if (reaction.Emote.Name == "📖")
                        {
                          
                           
                                await OctoGameUpdateMess.FighhtReaction.OctoGameLogs(reaction,
                                    Global.OctopusGameMessIdList[i].SocketMsg);
                            
                        }
                        else if (reaction.Emote.Name == "❌")
                        {
                            await OctoGameUpdateMess.FighhtReaction.EndGame(reaction, Global.OctopusGameMessIdList[i].SocketMsg);
                        }
                       
                        else if (reaction.Emote.Name == "1⃣")
                        {
                            //////////////////////////////////////////////////////////AD SKills////////////////////////////////////////////////////////////////////////////////

                            if (account.OctopusFightPlayingStatus == 1)
                            {
                                account.CurrentEnemyLvl = account.CurrentOctopusFighterLvl - 1;
                                account.CurrentEnemyStrength = 20;
                                account.CurrentEnemyAd = 0;
                                account.CurrentEnemyAp = 0;
                                account.CurrentEnemyAgility = 0;
                                account.CurrentEnemyDodge = 0;
                                account.CurrentEnemyCritDmg = 1.5;
                                account.CurrentEnemyArmor = 1;
                                account.CurrentEnemyMagicResist = 1;
                                account.CurrentEnemyHealth = 100;
                                account.CurrentEnemyStamina = 200;

                                account.OctopusFightPlayingStatus = 2;
                                GameUserAccounts.SaveAccounts();

                                await OctoGameUpdateMess.FighhtReaction.WaitMess(reaction,
                                    Global.OctopusGameMessIdList[i].SocketMsg);
                            }

                            if (account.OctopusFightPlayingStatus == 2)
                            {
                                string[] skills;
                               
                                double dmg ;
                                

                                if (account.MoveListPage == 1)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAd.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[0]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                   
                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Бууууль! ты победил!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }

                                    
                                    GameUserAccounts.SaveAccounts();

                                }
                         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        //////////////////////////////////////////////////////////////DEF Skill///////////////////////////////////////////////////////////////////////////
                                else  if (account.MoveListPage == 2)
                                {
                                                               
                                        skills = account.CurrentOctopusFighterSkillSetDef.Split(new[] {'|'},
                                            StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[0]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.DefdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                        if (account.CurrentEnemyStamina > 0)
                                        {
                                            account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();



                                            if (account.CurrentEnemyStamina < 0)
                                            {
                                                account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                                account.CurrentEnemyStamina = 0;
                                                GameUserAccounts.SaveAccounts();
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        else if (account.CurrentEnemyStamina <= 0)
                                        {

                                            account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();

                                            if (account.CurrentEnemyHealth <= 0)
                                            {
                                                await reaction.Channel.SendMessageAsync("Бууууль! ты победил!");
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }


                                        GameUserAccounts.SaveAccounts();
                                    }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////////////AGI skills///////////////////////////////////////////////////////////////////////////


                              else  if (account.MoveListPage == 3)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAgi.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[0]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AgiSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Бууууль! ты победил!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();
                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        ////////////////////////////////////////////////////////////////AP Skills//////////////////////////////////////////////////////////////////////
                       

                              else  if (account.MoveListPage == 4)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAp.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[0]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.ApSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Бууууль! ты победил!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            }
                           
                        }
                        else if (reaction.Emote.Name == "2⃣")
                        {
                            if (account.OctopusFightPlayingStatus == 1)
                            {
                                account.CurrentEnemyLvl = account.CurrentOctopusFighterLvl;
                                account.CurrentEnemyStrength = 30;
                                account.CurrentEnemyAd = 0;
                                account.CurrentEnemyAp = 0;
                                account.CurrentEnemyAgility = 0;
                                account.CurrentEnemyDodge = 0;
                                account.CurrentEnemyCritDmg = 1.5;
                                account.CurrentEnemyArmor = 1;
                                account.CurrentEnemyMagicResist = 1;
                                account.CurrentEnemyHealth = 100;
                                account.CurrentEnemyStamina = 300;
                                
                                account.OctopusFightPlayingStatus = 2;
                                GameUserAccounts.SaveAccounts();

                                await OctoGameUpdateMess.FighhtReaction.WaitMess(reaction,
                                    Global.OctopusGameMessIdList[i].SocketMsg);
                            }
                              if (account.OctopusFightPlayingStatus == 2)
                            {
                                string[] skills;
                               
                                double dmg;
                                

                                if (account.MoveListPage == 1)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAd.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[1]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AdSkills(skill.SpellId, account);
                                        dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                        dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                }
                         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        //////////////////////////////////////////////////////////////DEF Skill///////////////////////////////////////////////////////////////////////////
                                else  if (account.MoveListPage == 2)
                                {
                                                               
                                        skills = account.CurrentOctopusFighterSkillSetDef.Split(new[] {'|'},
                                            StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[1]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");


                                        dmg = GameSpellHandeling.DefdSkills(skill.SpellId, account);
                                        dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                        dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                

                                        if (account.CurrentEnemyStamina > 0)
                                        {
                                            account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();



                                            if (account.CurrentEnemyStamina < 0)
                                            {
                                                account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                                account.CurrentEnemyStamina = 0;
                                                GameUserAccounts.SaveAccounts();
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        else if (account.CurrentEnemyStamina <= 0)
                                        {

                                            account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();

                                            if (account.CurrentEnemyHealth <= 0)
                                            {
                                                await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }


                                        GameUserAccounts.SaveAccounts();
                                    }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////////////AGI skills///////////////////////////////////////////////////////////////////////////


                              else  if (account.MoveListPage == 3)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAgi.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[1]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AgiSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();
                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        ////////////////////////////////////////////////////////////////AP Skills//////////////////////////////////////////////////////////////////////
                       

                              else  if (account.MoveListPage == 4)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAp.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[1]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.ApSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            }
                           
                        }
                        else if (reaction.Emote.Name == "3⃣")
                        {



                            if (account.OctopusFightPlayingStatus == 1)
                            {
                                account.CurrentEnemyLvl = account.CurrentOctopusFighterLvl + 1;
                                account.CurrentEnemyStrength = 40;
                                account.CurrentEnemyAd = 0;
                                account.CurrentEnemyAp = 0;
                                account.CurrentEnemyAgility = 0;
                                account.CurrentEnemyDodge = 0;
                                account.CurrentEnemyCritDmg = 1.5;
                                account.CurrentEnemyArmor = 1;
                                account.CurrentEnemyMagicResist = 1;
                                account.CurrentEnemyHealth = 100;
                                account.CurrentEnemyStamina = 400;
                                
                                account.OctopusFightPlayingStatus = 2;
                                GameUserAccounts.SaveAccounts();
                                await OctoGameUpdateMess.FighhtReaction.WaitMess(reaction,
                                    Global.OctopusGameMessIdList[i].SocketMsg);
                            }
                            
                              if (account.OctopusFightPlayingStatus == 2)
                            {
                                string[] skills;
                               
                                double dmg ;
                                

                                if (account.MoveListPage == 1)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAd.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[2]); 
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                }
                         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        //////////////////////////////////////////////////////////////DEF Skill///////////////////////////////////////////////////////////////////////////
                                else  if (account.MoveListPage == 2)
                                {
                                                               
                                        skills = account.CurrentOctopusFighterSkillSetDef.Split(new[] {'|'},
                                            StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[2]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.DefdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                        if (account.CurrentEnemyStamina > 0)
                                        {
                                            account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();



                                            if (account.CurrentEnemyStamina < 0)
                                            {
                                                account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                                account.CurrentEnemyStamina = 0;
                                                GameUserAccounts.SaveAccounts();
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        else if (account.CurrentEnemyStamina <= 0)
                                        {

                                            account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();

                                            if (account.CurrentEnemyHealth <= 0)
                                            {
                                                await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }


                                        GameUserAccounts.SaveAccounts();
                                    }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////////////AGI skills///////////////////////////////////////////////////////////////////////////


                              else  if (account.MoveListPage == 3)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAgi.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[2]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AgiSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();
                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        ////////////////////////////////////////////////////////////////AP Skills//////////////////////////////////////////////////////////////////////
                       

                              else  if (account.MoveListPage == 4)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAp.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[2]);  
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.ApSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            }
                           
                        }
                        else if (reaction.Emote.Name == "4⃣")
                        {
                             if (account.OctopusFightPlayingStatus == 2)
                            {
                                string[] skills;
                               
                                double dmg ;
                                

                                if (account.MoveListPage == 1)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAd.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[3]); 
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                }
                         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        //////////////////////////////////////////////////////////////DEF Skill///////////////////////////////////////////////////////////////////////////
                                else  if (account.MoveListPage == 2)
                                {
                                                               
                                        skills = account.CurrentOctopusFighterSkillSetDef.Split(new[] {'|'},
                                            StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[3]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.DefdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                        if (account.CurrentEnemyStamina > 0)
                                        {
                                            account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();



                                            if (account.CurrentEnemyStamina < 0)
                                            {
                                                account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                                account.CurrentEnemyStamina = 0;
                                                GameUserAccounts.SaveAccounts();
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        else if (account.CurrentEnemyStamina <= 0)
                                        {

                                            account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();

                                            if (account.CurrentEnemyHealth <= 0)
                                            {
                                                await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }


                                        GameUserAccounts.SaveAccounts();
                                    }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////////////AGI skills///////////////////////////////////////////////////////////////////////////


                              else  if (account.MoveListPage == 3)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAgi.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[3]); 
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AgiSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();
                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        ////////////////////////////////////////////////////////////////AP Skills//////////////////////////////////////////////////////////////////////
                       

                              else  if (account.MoveListPage == 4)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAp.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[3]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.ApSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                } 
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            }
                        }
                        else if (reaction.Emote.Name == "5⃣")
                        {
                            if (account.OctopusFightPlayingStatus == 2)
                            {
                                string[] skills;
                               
                                double dmg ;
                                

                                if (account.MoveListPage == 1)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAd.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[4]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                }
                         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        //////////////////////////////////////////////////////////////DEF Skill///////////////////////////////////////////////////////////////////////////
                                else  if (account.MoveListPage == 2)
                                {
                                                               
                                        skills = account.CurrentOctopusFighterSkillSetDef.Split(new[] {'|'},
                                            StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[4]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.DefdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                        if (account.CurrentEnemyStamina > 0)
                                        {
                                            account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();



                                            if (account.CurrentEnemyStamina < 0)
                                            {
                                                account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                                account.CurrentEnemyStamina = 0;
                                                GameUserAccounts.SaveAccounts();
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        else if (account.CurrentEnemyStamina <= 0)
                                        {

                                            account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();

                                            if (account.CurrentEnemyHealth <= 0)
                                            {
                                                await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }


                                        GameUserAccounts.SaveAccounts();
                                    }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////////////AGI skills///////////////////////////////////////////////////////////////////////////


                              else  if (account.MoveListPage == 3)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAgi.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[4]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AgiSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();
                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        ////////////////////////////////////////////////////////////////AP Skills//////////////////////////////////////////////////////////////////////
                       

                              else  if (account.MoveListPage == 4)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAp.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[4]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.ApSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                } 
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            } 
                        }
                        else if (reaction.Emote.Name == "6⃣")
                        {
                             if (account.OctopusFightPlayingStatus == 2)
                            {
                                string[] skills;
                               
                                double dmg ;
                                

                                if (account.MoveListPage == 1)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAd.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[5]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                }
                         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        //////////////////////////////////////////////////////////////DEF Skill///////////////////////////////////////////////////////////////////////////
                                else  if (account.MoveListPage == 2)
                                {
                                                               
                                        skills = account.CurrentOctopusFighterSkillSetDef.Split(new[] {'|'},
                                            StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[5]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.DefdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                        if (account.CurrentEnemyStamina > 0)
                                        {
                                            account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();



                                            if (account.CurrentEnemyStamina < 0)
                                            {
                                                account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                                account.CurrentEnemyStamina = 0;
                                                GameUserAccounts.SaveAccounts();
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        else if (account.CurrentEnemyStamina <= 0)
                                        {

                                            account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();

                                            if (account.CurrentEnemyHealth <= 0)
                                            {
                                                await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }


                                        GameUserAccounts.SaveAccounts();
                                    }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////////////AGI skills///////////////////////////////////////////////////////////////////////////


                              else  if (account.MoveListPage == 3)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAgi.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[5]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AgiSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();
                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        ////////////////////////////////////////////////////////////////AP Skills//////////////////////////////////////////////////////////////////////
                       

                              else  if (account.MoveListPage == 4)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAp.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[5]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.ApSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                } 
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            }
                        }
                        else if (reaction.Emote.Name == "7⃣")
                        {
                             if (account.OctopusFightPlayingStatus == 2)
                            {
                                string[] skills;
                               
                                double dmg ;
                                

                                if (account.MoveListPage == 1)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAd.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[6]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                    dmg = GameSpellHandeling.AdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                }
                         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        //////////////////////////////////////////////////////////////DEF Skill///////////////////////////////////////////////////////////////////////////
                                else  if (account.MoveListPage == 2)
                                {
                                                               
                                        skills = account.CurrentOctopusFighterSkillSetDef.Split(new[] {'|'},
                                            StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[6]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.DefdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                        if (account.CurrentEnemyStamina > 0)
                                        {
                                            account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();



                                            if (account.CurrentEnemyStamina < 0)
                                            {
                                                account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                                account.CurrentEnemyStamina = 0;
                                                GameUserAccounts.SaveAccounts();
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        else if (account.CurrentEnemyStamina <= 0)
                                        {

                                            account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();

                                            if (account.CurrentEnemyHealth <= 0)
                                            {
                                                await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }


                                        GameUserAccounts.SaveAccounts();
                                    }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////////////AGI skills///////////////////////////////////////////////////////////////////////////


                              else  if (account.MoveListPage == 3)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAgi.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[6]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AgiSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();
                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        ////////////////////////////////////////////////////////////////AP Skills//////////////////////////////////////////////////////////////////////
                       

                              else  if (account.MoveListPage == 4)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAp.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[6]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.ApSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            }
                        } 
                        else if (reaction.Emote.Name == "8⃣")
                        {
                             if (account.OctopusFightPlayingStatus == 2)
                            {
                                string[] skills;
                               
                                double dmg ;
                                

                                if (account.MoveListPage == 1)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAd.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[7]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                }
                         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        //////////////////////////////////////////////////////////////DEF Skill///////////////////////////////////////////////////////////////////////////
                                else  if (account.MoveListPage == 2)
                                {
                                                               
                                        skills = account.CurrentOctopusFighterSkillSetDef.Split(new[] {'|'},
                                            StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[7]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.DefdSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                        if (account.CurrentEnemyStamina > 0)
                                        {
                                            account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();



                                            if (account.CurrentEnemyStamina < 0)
                                            {
                                                account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                                account.CurrentEnemyStamina = 0;
                                                GameUserAccounts.SaveAccounts();
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        else if (account.CurrentEnemyStamina <= 0)
                                        {

                                            account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                            GameUserAccounts.SaveAccounts();

                                            if (account.CurrentEnemyHealth <= 0)
                                            {
                                                await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                                await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                    Global.OctopusGameMessIdList[i].SocketMsg);
                                            }

                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,
                                                Global.OctopusGameMessIdList[i].SocketMsg);
                                        }


                                        GameUserAccounts.SaveAccounts();
                                    }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////////////AGI skills///////////////////////////////////////////////////////////////////////////


                              else  if (account.MoveListPage == 3)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAgi.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[7]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.AgiSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();
                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                
                        ////////////////////////////////////////////////////////////////AP Skills//////////////////////////////////////////////////////////////////////
                       

                              else  if (account.MoveListPage == 4)
                                {
                                    skills = account.CurrentOctopusFighterSkillSetAp.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                        var ski = Convert.ToUInt64(skills[7]);
                                        var skill = SpellUserAccounts.GetAccount(ski);

                                        Console.WriteLine($"{skill.SpellName} + {skill.SpellId}");

                                        dmg = GameSpellHandeling.ApSkills(skill.SpellId, account);
                                    dmg = GameSpellHandeling.CritHandeling(account.CurrentOctopusFighterAgility, dmg, account);
                                    dmg = GameSpellHandeling.DodgeHandeling(account.CurrentOctopusFighterAgility, dmg, account);

                                    if (account.CurrentEnemyStamina > 0)
                                    {
                                     account.CurrentEnemyStamina -= Math.Ceiling(dmg);
                                     GameUserAccounts.SaveAccounts();

                                       

                                        if (account.CurrentEnemyStamina < 0)
                                        {
                                            account.CurrentEnemyHealth += account.CurrentEnemyStamina;
                                            account.CurrentEnemyStamina = 0;
                                            GameUserAccounts.SaveAccounts();
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                    else if (account.CurrentEnemyStamina <= 0)
                                    {

                                        account.CurrentEnemyHealth -= Math.Ceiling(dmg);
                                        GameUserAccounts.SaveAccounts();
                                      
                                        if (account.CurrentEnemyHealth <= 0)
                                        {
                                            await reaction.Channel.SendMessageAsync("Buuuuul! You won!");
                                            await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                        }
                                        await OctoGameUpdateMess.FighhtReaction.MainPage(reaction,  Global.OctopusGameMessIdList[i].SocketMsg);
                                    }
                                  
                                   
                                    GameUserAccounts.SaveAccounts();

                                }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            }
                        }
                   
                    }         
            }
        }


    }
}
