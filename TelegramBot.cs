using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class MyBot
    {
        #region Variables
        private static readonly TelegramBotClient botClient = new TelegramBotClient("token");

        private static Dictionary<long, String> userChoices = new Dictionary<long, string>();

        private static List<FileToSend> mainImgs = new List<FileToSend>();
        private static List<List<FileToSend>> questionImgs = new List<List<FileToSend>>();
        
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
                                                    new KeyboardButton("Лаба \U00000031"),
                                                    new KeyboardButton("Лаба \U00000032")
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
        #endregion

        public static void Main(string[] args)
        {
            LoadFiles();
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


                    await botClient.SendPhotoAsync(
                    message.Chat.Id,
                    mainImgs[0],
                    "HI THERE"
                    );
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "CHOOSE YOUR DESTINY",
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
                                                                  "\n*UNSIGNED BYTE*  [0..255]" +
                                                                  "\n*SIGNED WORD*  [-32768..32767]" +
                                                                  "\n*UNSIGNED WORD* [0..65535]" +
                                                                  "\n*SIGNED DWORD*  [-2147483648..2147483647]" +
                                                                  "\n*UNSIGNED DWORD*  [0..4294967295]",
                                                                  parseMode: ParseMode.Markdown);

                    break;


                case "команды преобразования":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "*CBW* - Преобразование байта в слово ( AL -> AX )"
                                                                 + "\n*CWD* - Преобразовать слово в двойное слово(AX->DX:AX)"
                                                                 + "\n*CWDE* - Преобразовать AX в EAX"
                                                                 + "\n*CDQ* - Преобразовать EAX в EDX: EAX",
                                                                 parseMode: ParseMode.Markdown);
                    break;
                case "команды передачи управления":
                    await botClient.SendTextMessageAsync(message.Chat.Id,
                    "*JMP* MARK - переходит на метку MARK\n" +
                    "*CMP* A, B - сравнивает A и B",
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
                                                                 + "Если K - байт, второй сомножитель в AL, результат в AX\n"
                                                                 + "Если K - слово, второй сомножитель в AX, результат в DX: AX\n"
                                                                 + "Если K - двойное слово, второй сомножитель в EAX, результат в EDX: EAX",
                                                                 parseMode: ParseMode.Markdown,
                                                                 replyMarkup: arithmMulExamplesKeybord);
                    break;
                case "\U00002797 деление":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "*DIV/IDIV K*\n"
                                                                 + "Если K - байт, делимое в AX, результат: частное в AL, остаток в AH\n"
                                                                 + "Если K - слово, делимое в DX:AX, результат: частное в AX, остаток в DX\n"
                                                                 + "Если K - двойное слово, делимое в EDX:EAX, результат: частное в EAX, остаток в EDX",
                                                                 parseMode: ParseMode.Markdown,
                                                                 replyMarkup: arithmDivExamplesKeybord);
                    break;
                case "\U00002795 \U00002796 сложение/вычитание":
                    await botClient.SendTextMessageAsync(message.Chat.Id,
                                                         "*ADD A,B* - прибавляет B к A\n" +
                                                         "*SUB A,B* - вычитает B из A",
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
                        "CHOOSE YOUR DESTINY",
                        false,
                        0,
                        mainKeyboard);
                        userChoices[message.From.Id] = "";
                    }
                    break;
                case "лаба \U00000031":
                    await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Тип данных?",
                    replyMarkup: lab1ExamplesKeyboard
                    );
                    break;
                case "лаба \U00000032":
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
                        catch (Exception e)
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
                default:
                    break;
            }

        }

        private static void LoadFiles()
        {
            string temp = "";
            int tempCount = -1;
            String[] tempArray;

            StreamReader streamReader;

            Console.WriteLine("Loading files...");

            using (streamReader = new StreamReader("mainImgs.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    temp = streamReader.ReadLine();
                    mainImgs.Add(new FileToSend(temp));
                }
            }

            using (streamReader = new StreamReader("questionImgs.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    tempCount++;
                    questionImgs.Add(new List<FileToSend>());
                    tempArray = streamReader.ReadLine().Split(' ');
                    for(int t = 0; t < tempArray.Length; t++)
                    {
                        questionImgs[tempCount].Add(new FileToSend(tempArray[t]));
                    }
                }
            }

            Console.WriteLine("All files successful loaded!");
        }
    }
}
