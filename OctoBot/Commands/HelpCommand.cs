using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Handeling;
using OctoBot.Services;

namespace OctoBot.Commands
{


    public class HelpCommand : ModuleBase<SocketCommandContextCustom>
    {


        [Command("ping228")]

        public async Task PingAync()
        {
            
            //G1024Provider.MakeMove(Context.User.Id, Game1024.MoveDirection.Up);
            var embed = new EmbedBuilder();
            embed.WithTitle("Bonafak")
                    .WithDescription("wanna fak?")
                    .WithColor(Color.Blue);
            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
            }
        }

        [Command("help")]
        [Alias("Помощь")]
        public async Task Help()
        {
            var embed = new EmbedBuilder();


            embed.AddField("Основные Команды: ", "**octo** Покажет рандомную осьминогу~\n" +
                                                 "**записать [имя]** Записать факт о юзере, для команды **факт**\n" +
                                                 "**факт [имя]** Показать рандомный факт о юзере\n" +
                                                 "**stats** Посмотреть свои статы у OctoBot\n" +
                                                 "**top** топ юзеров по OctoPoints, tops - по подписчиками, top [number] - выбрать страницу\n" +
                                                 "**pull** Ежедневневный поинт, собрав 20 получите ключ игры в ЛС, пропустите 1 день - потеряете все поинты.\n" +
                                                 "**AddKey [любой текст]** добавить ключ в общий пулл ключей для pull\n" +
                                                 "**угадайка [ставка]** Рулетка. Бот говорит сколько слотов, вы дожны выбрать 1 слот написав второе сообщение после бота\n" +
                                                 "**AllOcto** Помощь о том, как дарить осьминогов другим а так же посмотреть всех осьминожек!\n" +
                                                 "**---------------------------------------------**\n" +
                                                 "_______\n");

            embed.AddField("Remind команды",
                "**Remind [Любой текст] через [время]** Напомнит вам то, что вы попросили через некоторое время Личный Сообщением, введите **HelpRemind** для инфы об этой команде. Пример: **Напомнить буль-буль через 1d 1h 1m 15s**\n" +
                "**Remind [User ID] [Любой текст] через [время]** Напомнить другому человеку ( человек увидит кто сделал этот ремайндер)\n" +
                "**Re [цифра-в-минутах] [Любой текст]** Cокрощенная команда, напомнит вам через минуты которые вы указалаи\n" +
                "**DM** Все команды включая напоминания можно писать в личку боту, __ВАЖНО__ команды нужно писать без какого либо префикса\n" +
                "**---------------------------------------------**\n" +
                "_______ \n");                         
                                               

            embed.AddField("Help команды",
                                                 "**HelpFull** Показать **Допкоманды 1** команды бота\n" +
                                                 "**HelpExtra** Показать **Допкоманды 2** команды бота" +
                                                 "**HelpRemind** Помощь по команде **Remind**\n" +
                                                 "**HelpPass** Информация о пассах и доступах\n" +
                                                 "**HelpBlog** Информация о системе Блогов (сабов)\n" +
                                                 "**HelpMod** Модерские команды\n" +
                                                 " \n" +
                                                 "**_______**\n" +
                                                 "**mylorik** это создатель бота, можете ему написать в ЛС если есть вопросы или предложения\n" +
                                                 "Кстати, Вместо префикса можно пинговать бота. или писать ему в ЛС без префикса или пинга");        
      
            embed.WithFooter("Записная книжечка Осьминожек");
            embed.WithColor(Color.LightOrange);
            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
            }
        }
       
        [Command("HelpFull")]
        [Alias("Help Full", "HelpAll", "Help All", "fullhelp", "full help", "all help", "allhelp")]
        public async Task HelpFull()
        {
            var embed = new EmbedBuilder();


            embed.AddField("Остальные команды:",
                "**пасс** или **pass** Покупка пропуска который открывает некоторые из команд.\n" +
                "**quote [имя]** Не крутая цитата человка\n" +
                "**2048** Запускает игру 2048 для юзера\n" +
                "**DM** Начать разговор с ботов в ЛС\n" +
                "**roll [номер]** Рандомно выдаст номер от 0 до числа, которые вы ввели\n" +
                "**roll [номер][X]** Рандомно выдаст номер от 0 до числа, которые вы ввели X раз\n" +
                "**pick [опция1...] | [опция2...]** Выбирает вместо вас, разделение между выборами `|` например `*pick пойти спать сейчас | через час | не спать` `опций может быть сколько угодно`\n" +
                "**---------------------------------------------**\n" +
                "_______ \n");
            embed.AddField("Местные мемы: ( писать без префикса)","Кто там?\n" +
                                                                 "я проиграл\n" +
                                                                 "заповедь\n" +
                                                                 "*А там\n" +
                                                                  "Бот начилсяет ОктоПоинты за актив, ее можно тратить на покупки осьминожек!");
        
           
           

            embed.WithFooter("Записная книжечка Осьминожек");
            embed.WithColor(Color.LightOrange);
            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
            }


            // embed.AddField("цитата [имя] [текст...] ", "Фейковая цитата от @юзера (*скриншот* с цветом, аватаркой, и текст)"); //font is NOT INCLUDET
            // command CARE
            // embed.AddField("Ping ", "Проверить свой пинг к боту");
            // embed.AddField("капча [текст...]", "Фейковая капча\n" +
        }


        [Command("HelpRemind")]
        [Alias("Remind Help", "HelpRemind", "Help Remind ", "remindhelp", "remind help")]
        public async Task HelpRemaind()
        {
            var embed = new EmbedBuilder();
            


          
            embed.AddField("Напоминания", "**Remind [Любой текст] через [время]** чтобы создать напоминание\n" +
                                          "**Remind [User ID] [Любой текст] через [время]** Напомнить **другому человеку** ( человек увидит кто сделал этот ремайндер)\n" +
                                          "**Re [цифра-в-минутах] [Любой текст]** сокрощенная команда, напомнит вам через минуты которые вы указалаи\n" +
                                          "__**через**__ это слово должно быть всегда между вашим текстом и времем, это переход.\n" +
                                          "**---------------------------------------------**\n" +
                                          "_______\n");
           
            embed.AddField("**ВРЕМЯ** день-час-минута-секунда", "Любую из частей времени можно не писать, но остальные писать по порядку.\n" +
                                                                "Один пробел между каждой частью **ИЛИ** без пробела вообще, например **1h30m** или **1d 3h 15m 30s** или **15m 44s**.\n" +
                                                                "максимальные значения таковы: `23h` `59m` `59s`\n" +
                                                                "Бот вас оповестит если команда сработала.\n" +
                                                                "**---------------------------------------------**\n" +
                                                                " _______\n");
           
            embed.AddField("Дополнительно:", "если напоинмание записалось - бот об этом оповестит.\nБот присылает личное сообщение, по окончанию таймера\n" +
                                             "**Все** напоминания будут работать чтобы не случилось, даже если бот не будет работать некоторое время.\n" +
                                             "`List` `Delete индекс_напоминания` `время ИЛИ date`\n" +
                                             "**Боту можно писать в Личку, но при этом НЕ НУЖНО ипользовать**\n" +
                                             "**Псевдонимы:** Вместо `Remind` можно писать `напиши мне` или `напомни` или  `алярм` или `Напомнить` или `напомни мне`\n" +
                                             "**---------------------------------------------**\n" +
                                             " _______\n");

            embed.AddField("List", "Покажит вам все ваши напоминания вместе с индексом который нужен для удаления напоминаний, если они вам больше не нужны");
            embed.AddField("Delete [индекс]", " Удаляет напоминание, где **индекс** это напоминание которое вы хотите удалить");
            embed.AddField("Время или Date", "Покажет текущее UTC время");
           
            embed.WithFooter("Записная книжечка Осьминожек");
            embed.WithColor(Color.LightOrange);
            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
            }
        }


        
        [Command("HelpMod")]
        [Alias("Mod Help", "Moderation", "AdminHelp", "modhelp", "help mod")]
        public async Task HelpAdmin()
        {
            var embed = new EmbedBuilder();
            embed.AddField("OctoPoint [имя] [номер] ", "Начисляет ОктоПоинты, местная валюта за которую можно что то купить ");
            embed.AddField("OctoRep [имя] [номер]", "Дает поинты свега, просто так, пока что");
            embed.AddField("УдалитьФакт [имя] [индекс]", "Удалит определенный факт");
            embed.AddField("warn [предупреждение]", "Записывает в аккаунт заслуги. Псевдонимы: `варн` `warning` `предупреждение`");
            embed.AddField("keys", "посмотреть свою ключи с Pull");
            embed.AddField("KeyDel [index]", "удалить ключ под индексом.");
            embed.AddField("purge [номер сообщений]", "Удалить сообщения. Псевдонимы: `clean` `clear` `убрать`");
            embed.AddField("ban, kick, mute", "ну и другие команды для менеджмента сервера");
            embed.AddField("Персональные команды:", "VollGaz, YellowTurtle, GreenBuu");
          
            embed.WithFooter("Записная книжечка Осьминожек");
            embed.WithColor(Color.LightOrange);
            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
            }
        }


        [Command("HelpPass")]
        [Alias("Pass Help", "Help Pass", "PassHelp")]
        public async Task HelpPass()
        {
            var embed = new EmbedBuilder();


            embed.AddField("Система допусков(пассов)", "Пропуски дают больше привилегий у осьминожек!\n" +
                                                       "**пасс** или **pass** Чтобы купить пропуск (бот у вас переспросит, так что он не сразу снимает поинты)\n" +
                                                       " \n" +
                                                       "**Доступ #1**\n" +
                                                       "**octo [индекс]** показать осьминожку под индексом\n**GiftPoints [username] [points]** Передать свои поинты другому с 10% комиссией\n" +
                                                       " \n" +
                                                       "**Доступ #2**\n" +
                                                       "**ВсеФакты** покажет ваши все факты\n**УдалитьФакт [индекс]** Удалит определенный факт\n" +
                                                       " \n" +
                                                       "**Доступ #3**\n" +
                                                       "**факт [имя] [индекс]** Показать факт о юзере под индексом\n" +
                                                       " \n" +
                                                       "**Доступ #4**\n" +
                                                       "**ВсеФакты [имя]** покажет все факты юзера\n**stats [имя]** Статистика юзверя и все его варны, кики, баны и другие засгули\n" +
                                                       " \n" +
                                                       "**Доступ #100**\n" +
                                                       $"**осьминожка** {new Emoji("🐙")}");
            

            embed.WithFooter("Записная книжечка Осьминожек");
            embed.WithColor(Color.LightOrange);
            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
            }
        }


        [Command("HelpBlog")]
        [Alias("Help Sub", "SubHelp", "HelpSub")]
        public async Task HelpSub()
        {
            var embed = new EmbedBuilder();

    
   
            

            embed.AddField("Система Блогов(Сабов)", "вы можете подписаться на человека, и когда тот пропишет команду **blog [что-то]** вы получите" +
                                                    "ЛС с этим [что-то] от осьминожек!\n" +
                                                    "Так же под блогами будут оценки, и ваши подписчики могут оценивать ваши работы от 1 до 5, средний бал можно увидеть по команде **topr**" +
                                                    "**---------------------------------------------**\n" +
                                                    " _______\n");
            embed.AddField("Команды:", "**Sub [user]** Подписаться на юзера\n" +
                                       "**Unsub [user]** Отписаться на юзера\n" +                                
                                       "**Blog [любой текст]** Все ваши подписчики получат ЛС в таком формате [ваш ник]: [любой текст]\n" +
                                       "**Iblog [image url] [любой текст]** Тот же блог, но с картинкой\n" +
                                       "**Subs** или **MySubs** или **подписки** посмотреть на кого ты подписан\n" +
                                       "**Subc** или **MySubc** или **подписчики** посмотреть своих подписчиков\n" +
                                       "**topSub** Покажет топы у кого больше всего сабов");
           
            embed.WithFooter("Записная книжечка Осьминожек");
            embed.WithColor(Color.Blue);
            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
            }


        }

        [Command("HelpExtra")]
        [Alias("Help Extra", "HelpE")]
        public async Task HelpExtra()
        {
            var embed = new EmbedBuilder();
            embed.AddField("Топы:", "По дефолту показывает первую страницу, страницу можно указать дописав номер страницы после команды\n" +
                                    "**top** Топ по Активу сервера\n" +
                                    "**topp** Топ по ОктоПоинтам\n" +
                                    "**tops** Топ по количеству подписчиков\n" +
                                    "**topr** Топ по рейтингу Блогов (подробно по команде HelpBlog)\n" +
                                    "**topa** Топ по рейтингу сообщений, этот рейтинг можно вызвать под каждым сообщением поставив одну из этих эмоций (:bar_chart: :art: :trophy: :frame_photo:)");
            embed.AddField("Extra Commands", "**GiftPoints [User] [number]** Передать свои поинты другому -10% комиссия\n" +
                                             "**OctoPoint [User] [number]**  Дать Окто Поинты(админ)\n" +
                                             "**OctoRep [User] [number]** Дать Окто Репу(админ)");
            embed.AddField("Extra:", "Все доступные роли можно получить в #info нажам на определенную реакцию\n");
            embed.WithFooter("Записная книжечка Осьминожек");
            embed.WithColor(Color.LightOrange);
            if (Context.MessageContentForEdit != "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed);
  
            }
            else if(Context.MessageContentForEdit == "edit")
            {
                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embed, "edit");
            }
        }
        
    }

}
