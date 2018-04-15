using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using System.Diagnostics;

namespace TelegramBot
{
    class MyBot
    {
        #region Variables
        private static readonly TelegramBotClient botClient = new TelegramBotClient("TOKEN");

        private static Dictionary<long, string> userChoices;
        public static List<FileToSend> mainImgs = new List<FileToSend>();
        public static List<List<FileToSend>> questionImgs = new List<List<FileToSend>>();
        #endregion

        #region Keyboards
        static ReplyKeyboardMarkup mainKeyboard = new ReplyKeyboardMarkup
        {
            Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton("\U0001F4D1 Программа"),
                                                    new KeyboardButton("\U00002753 Вопрос")
                                                },
                                            },
            ResizeKeyboard = true
        };

        static ReplyKeyboardMarkup progKeyboard = new ReplyKeyboardMarkup
        {
            Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton("Типы данных"),
                                                    new KeyboardButton("Арифметические операции")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("Команды преобразования"),
                                                    new KeyboardButton("Команды передачи управления")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("\U0001F4D6 Примеры"),
                                                    new KeyboardButton("\U000023EA Назад")
                                                },
                                            },
            ResizeKeyboard = true
        };

        static InlineKeyboardMarkup typesCommandsTransferManaging = new InlineKeyboardMarkup(new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Команды для беззнаковых типов", "transEx_US"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Команды для знаковых типов", "transEx_S"),
                        },
                    });

        static ReplyKeyboardMarkup arithmKeyboard = new ReplyKeyboardMarkup
        {
            Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton("\U0000274C Умножение"),
                                                    new KeyboardButton("\U00002797 Деление")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("\U00002795 \U00002796 Сложение/Вычитание"),
                                                    new KeyboardButton("\U000023EA Назад")
                                                },
                                            },
            ResizeKeyboard = true
        };

        static InlineKeyboardMarkup arithmMulExamplesKeybord = new InlineKeyboardMarkup(new[]
                {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Пример", "mulEx"),
                        },
                });

        static InlineKeyboardMarkup arithmDivExamplesKeybord = new InlineKeyboardMarkup(new[]
                {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Пример", "divEx"),
                        },
                });

        static InlineKeyboardMarkup arithmPlusMinusExamplesKeybord = new InlineKeyboardMarkup(new[]
                {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Пример", "plusMinusEx"),
                        },
                });

        static ReplyKeyboardMarkup programExamplesKeyboard = new ReplyKeyboardMarkup
        {
            Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton("Лаб. № 1"),
                                                    new KeyboardButton("Лаб. № 2")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("\U000023EA Назад"),
                                                },
                                            },
            ResizeKeyboard = true
        };

        static InlineKeyboardMarkup lab1ExamplesKeyboard = new InlineKeyboardMarkup(new[]
                {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("UNSIGNED CHAR", "unsignedCharL1"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("SIGNED CHAR", "signedCharL1"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("UNSIGNED INT", "unsignedIntL1"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("SIGNED INT", "signedIntL1"),
                        },
                });

        static InlineKeyboardMarkup lab2ExamplesKeyboard = new InlineKeyboardMarkup(new[]
                {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("UNSIGNED INT", "unsignedIntL2"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("SIGNED INT", "signedIntL2"),
                        },
                });

        static ReplyKeyboardMarkup questionsKeyboard = new ReplyKeyboardMarkup
        {
            Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton("\U0001F4CB Список"),
                                                    new KeyboardButton("\U000023EA Назад")
                                                },
                                            },
            ResizeKeyboard = true
        };

        static InlineKeyboardMarkup typesOfDataKeyboard = new InlineKeyboardMarkup(new[]
        {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Регистры", "registers"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Директивы", "directives"),
                        },
                });

        static InlineKeyboardMarkup conversationCommandsKeyboard = new InlineKeyboardMarkup(new[]
{
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Подробнее", "goFurtherConversation"),
                        },
                });
        #endregion

        public static void Main(string[] args)
        {
            System.Timers.Timer restartTimer = new System.Timers.Timer();
            restartTimer.Interval = 86400000;
            restartTimer.Elapsed += new ElapsedEventHandler(restartTimer_Tick);
            restartTimer.Start();
            Console.WriteLine($"Bot restarts every {restartTimer.Interval/3600000} hours.");

            FileSystem.LoadFiles();
            userChoices = FileSystem.LoadUserChoices();

            Thread.Sleep(100);

            var me = botClient.GetMeAsync().Result;
            Console.Title = me.Username;

            botClient.OnMessage += BotOnMessageReceived;
            botClient.OnCallbackQuery += BotOnCallbackQueryReceived;

            botClient.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();



            botClient.StopReceiving();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            if (!FileSystem.ignoreMessagesLog.Contains(message.Text.Trim()))
            {
                FileSystem.GetLogs(message.Date.ToShortDateString() + " "
                                 + message.Date.ToShortTimeString(),
                                   message.From.Username,
                                   message.Text);
            }

            if (userChoices.ContainsKey(message.From.Id))
            {

            }
            else
            {
                userChoices.Add(message.From.Id, "");
            }

            switch (message.Text.ToLower())
            {
                case "/start":
                    await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                    await Task.Delay(200);

                    userChoices[message.From.Id] = "";

                    await botClient.SendPhotoAsync(
                    message.Chat.Id,
                    mainImgs[0],
                    "Привет! Я помогу тебе сдать Assемблер."
                    );
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Выбирай раздел",
                    ParseMode.Default,
                    false,
                    false,
                    0,
                    mainKeyboard
                    );
                    break;


                case "\U0001F4D1 программа":
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Выберите тему",
                    ParseMode.Default,
                    false,
                    false,
                    0,
                    progKeyboard
                    );
                    break;


                case "типы данных":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "*SIGNED BYTE*  [-128..127]" +
                                                                  "\n\n*UNSIGNED BYTE*  [0..255]" +
                                                                  "\n\n*SIGNED WORD*  [-32768..32767]" +
                                                                  "\n\n*UNSIGNED WORD* [0..65535]" +
                                                                  "\n\n*SIGNED DWORD*  [-2147483648..2147483647]" +
                                                                  "\n\n*UNSIGNED DWORD*  [0..4294967295]",
                                                                  parseMode: ParseMode.Markdown,
                                                                  replyMarkup: typesOfDataKeyboard);

                    break;


                case "команды преобразования":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "*CBW* - Преобразование байта в слово ( AL -> AX )"
                                                                 + "\n\n*CWD* - Преобразовать слово в двойное слово(AX->DX:AX)"
                                                                 + "\n\n*CWDE* - Преобразовать AX в EAX"
                                                                 + "\n\n*CDQ* - Преобразовать EAX в EDX: EAX",
                                                                 parseMode: ParseMode.Markdown,
                                                                 replyMarkup: conversationCommandsKeyboard);
                    break;
                case "команды передачи управления":
                    await botClient.SendTextMessageAsync(message.Chat.Id,
                    "*JMP* MARK - переходит на метку MARK\n" +
                    "\n*CMP* A, B - сравнивает A и B",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: typesCommandsTransferManaging);
                    break;
                case "арифметические операции":
                    userChoices[message.From.Id] = "арифметические операции";
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Выберите операцию",
                    ParseMode.Default,
                    false,
                    false,
                    0,
                    arithmKeyboard);
                    break;
                case "\U0000274C умножение":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "*MUL/IMUL K*\n"
                                                                 + "\nЕсли K - байт, второй сомножитель в AL, результат в AX\n"
                                                                 + "\nЕсли K - слово, второй сомножитель в AX, результат в DX: AX\n"
                                                                 + "\nЕсли K - двойное слово, второй сомножитель в EAX, результат в EDX: EAX",
                                                                 parseMode: ParseMode.Markdown,
                                                                 replyMarkup: arithmMulExamplesKeybord);
                    break;
                case "\U00002797 деление":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "*DIV/IDIV K*\n"
                                                                 + "\nЕсли K - байт, делимое в AX, результат: частное в AL, остаток в AH\n"
                                                                 + "\nЕсли K - слово, делимое в DX:AX, результат: частное в AX, остаток в DX\n"
                                                                 + "\nЕсли K - двойное слово, делимое в EDX:EAX, результат: частное в EAX, остаток в EDX",
                                                                 parseMode: ParseMode.Markdown,
                                                                 replyMarkup: arithmDivExamplesKeybord);
                    break;
                case "\U00002795 \U00002796 сложение/вычитание":
                    await botClient.SendTextMessageAsync(message.Chat.Id,
                                                         "*ADD A,B* - прибавляет B к A\n" +
                                                         "\n*SUB A,B* - вычитает B из A",
                                                         parseMode: ParseMode.Markdown,
                                                         replyMarkup: arithmPlusMinusExamplesKeybord);
                    break;
                case "\U0001F4D6 примеры":
                    userChoices[message.From.Id] = "примеры";
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Какая лабораторная?",
                    replyMarkup: programExamplesKeyboard
                    );
                    break;
                case "\U000023EA назад":
                    if (userChoices[message.From.Id] == "арифметические операции")
                    {
                        await botClient.SendTextMessageAsync(
                        message.Chat.Id,
                        "Выберите тему",
                        ParseMode.Default,
                        false,
                        false,
                        0,
                        progKeyboard
                        );
                        userChoices[message.From.Id] = "";
                    }
                    else if (userChoices[message.From.Id] == "примеры")
                    {
                        await botClient.SendTextMessageAsync(
                        message.Chat.Id,
                        "Выберите тему",
                        ParseMode.Default,
                        false,
                        false,
                        0,
                        progKeyboard
                        );
                        userChoices[message.From.Id] = "";
                    }
                    else
                    {
                        await botClient.SendPhotoAsync(
                        message.Chat.Id,
                        mainImgs[1],
                        "Выберите раздел",
                        false,
                        0,
                        mainKeyboard);
                        userChoices[message.From.Id] = "";
                    }
                    break;
                case "лаб. № 1":
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Тип данных?",
                    replyMarkup: lab1ExamplesKeyboard
                    );
                    break;
                case "лаб. № 2":
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Тип данных?",
                    replyMarkup: lab2ExamplesKeyboard
                    );
                    break;
                case "\U00002753 вопрос":
                    userChoices[message.From.Id] = "вопрос";
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Введите номер интересующего вас билета",
                    replyMarkup: questionsKeyboard
                    );
                    break;
                case "\U0001F4CB список":
                    await botClient.SendPhotoAsync(
                       message.Chat.Id,
                       mainImgs[13],
                       "Список билетов",
                       false,
                       0);
                    break;
                default:
                    if (userChoices[message.From.Id] == "вопрос")
                    {
                        int ticket = 0;
                        try
                        {
                            ticket = Convert.ToInt32(message.Text.Trim());
                        }
                        catch (Exception)
                        {
                            await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                            await Task.Delay(200);
                            await botClient.SendTextMessageAsync(
                                message.Chat.Id,
                                "Неверно введен номер билета");
                            break;
                        };
                        if (ticket > 0 && ticket <= 16)
                        {
                            for (int i = 0; i < questionImgs[ticket - 1].Count; i++)
                            {
                                await botClient.SendPhotoAsync(
                                   message.Chat.Id,
                                   questionImgs[ticket - 1][i],
                                   $"Билет №{ticket}.{i + 1}",
                                   false,
                                   0);
                            }
                        }
                        else
                        {
                            await Task.Delay(50);
                            await botClient.SendTextMessageAsync(
                                message.Chat.Id,
                                "Неверно введен номер билета");
                        }
                    }
                    break;
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;
            switch (callbackQuery.Data)
            {
                case "mulEx":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        mainImgs[2]
                        );
                    break;
                case "divEx":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        mainImgs[3]
                        );
                    break;
                case "plusMinusEx":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        mainImgs[4]
                        );
                    break;
                case "transEx_US":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        mainImgs[5]
                        );
                    break;
                case "transEx_S":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        mainImgs[6]
                        );
                    break;
                case "unsignedCharL1":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        mainImgs[7]
                        );
                    break;
                case "signedCharL1":
                    await botClient.SendPhotoAsync(
                        callbackQuery.Message.Chat.Id,
                        mainImgs[8]
                        );
                    break;
                case "unsignedIntL1":
                    await botClient.SendPhotoAsync(
                         callbackQuery.Message.Chat.Id,
                         mainImgs[9]
                         );
                    break;
                case "signedIntL1":
                    await botClient.SendPhotoAsync(
                         callbackQuery.Message.Chat.Id,
                         mainImgs[10]
                         );
                    break;
                case "unsignedIntL2":
                    await botClient.SendPhotoAsync(
                         callbackQuery.Message.Chat.Id,
                         mainImgs[11]
                         );
                    break;
                case "signedIntL2":
                    await botClient.SendPhotoAsync(
                         callbackQuery.Message.Chat.Id,
                         mainImgs[12]
                         );
                    break;
                case "registers":
                    await botClient.SendPhotoAsync(
                         callbackQuery.Message.Chat.Id,
                         mainImgs[14]
                         );
                    break;
                case "directives":
                    await botClient.SendPhotoAsync(
                         callbackQuery.Message.Chat.Id,
                         mainImgs[15]
                         );
                    break;
                case "goFurtherConversation":
                    await botClient.SendPhotoAsync(
                         callbackQuery.Message.Chat.Id,
                         mainImgs[16]
                         );
                    break;
                default:
                    break;
            }

        }

        static void restartTimer_Tick(Object sender, EventArgs e)
        {
            Process[] localProccesses = Process.GetProcesses();
            try
            {
                FileSystem.SaveUserChoices();
                FileSystem.logWriter.Flush();
                Process targetProcess = localProccesses
                                        .First(x => x.ProcessName == "TelegramBot");
                Process.Start("TelegramBot.exe");
                targetProcess.Kill();
            }
            catch
            {
                Console.WriteLine("Restart failed!");
            }
        }
    }
}