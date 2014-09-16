using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMi9Code.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net;
using Nito.AsyncEx.UnitTests;
using System.Web.Http.Hosting;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers; 

namespace MyMi9Code.Controllers.Tests
{
    [TestClass()]
    public class RequestDataControllerTests
    {
        //Init
        private string validRequestJson = "{\r\n    \"payload\": [\r\n        {\r\n            \"country\": \"UK\",\r\n            \"description\": \"What's life like when you have enough children to field your own football team?\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 3,\r\n            \"genre\": \"Reality\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/16KidsandCounting1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": null,\r\n            \"primaryColour\": \"#ff7800\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/16kidsandcounting/season/1\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/16kidsandcounting\",\r\n            \"title\": \"16 Kids and Counting\",\r\n            \"tvChannel\": \"GEM\"\r\n        },\r\n        {\r\n            \"slug\": \"show/seapatrol\",\r\n            \"title\": \"Sea Patrol\",\r\n            \"tvChannel\": \"Channel 9\"\r\n        },\r\n        {\r\n            \"country\": \" USA\",\r\n            \"description\": \"The Taste puts 16 culinary competitors in the kitchen, where four of the World's most notable culinary masters of the food world judges their creations based on a blind taste. Join judges Anthony Bourdain, Nigella Lawson, Ludovic Lefebvre and Brian Malarkey in this pressure-packed contest where a single spoonful can catapult a contender to the top or send them packing.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 2,\r\n            \"genre\": \"Reality\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/TheTaste1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": {\r\n                \"channel\": null,\r\n                \"channelLogo\": \"http://catchup.ninemsn.com.au/img/player/logo_go.gif\",\r\n                \"date\": null,\r\n                \"html\": \"<br><span class=\\\"visit\\\">Visit the Official Website</span></span>\",\r\n                \"url\": \"http://go.ninemsn.com.au/\"\r\n            },\r\n            \"primaryColour\": \"#df0000\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/thetaste/season/1\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/thetaste\",\r\n            \"title\": \"The Taste\",\r\n            \"tvChannel\": \"GEM\"\r\n        },\r\n        {\r\n            \"country\": \"UK\",\r\n            \"description\": \"The series follows the adventures of International Rescue, an organisation created to help those in grave danger using technically advanced equipment and machinery. The series focuses on the head of the organisation, ex-astronaut Jeff Tracy, and his five sons who piloted the \\\"Thunderbird\\\" machines.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 24,\r\n            \"genre\": \"Action\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/Thunderbirds_1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": null,\r\n            \"primaryColour\": \"#0084da\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/thunderbirds/season/1\"\r\n                },\r\n                {\r\n                    \"slug\": \"show/thunderbirds/season/3\"\r\n                },\r\n                {\r\n                    \"slug\": \"show/thunderbirds/season/4\"\r\n                },\r\n                {\r\n                    \"slug\": \"show/thunderbirds/season/5\"\r\n                },\r\n                {\r\n                    \"slug\": \"show/thunderbirds/season/6\"\r\n                },\r\n                {\r\n                    \"slug\": \"show/thunderbirds/season/8\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/thunderbirds\",\r\n            \"title\": \"Thunderbirds\",\r\n            \"tvChannel\": \"Channel 9\"\r\n        },\r\n        {\r\n            \"country\": \"USA\",\r\n            \"description\": \"A sleepy little village, Crystal Cove boasts a long history of ghost sightings, poltergeists, demon possessions, phantoms and other paranormal occurrences. The renowned sleuthing team of Fred, Daphne, Velma, Shaggy and Scooby-Doo prove all of this simply isn't real, and along the way, uncover a larger, season-long mystery that will change everything.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 4,\r\n            \"genre\": \"Kids\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/ScoobyDoo1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": null,\r\n            \"primaryColour\": \"#1b9e00\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/scoobydoomysteryincorporated/season/1\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/scoobydoomysteryincorporated\",\r\n            \"title\": \"Scooby-Doo! Mystery Incorporated\",\r\n            \"tvChannel\": \"GO!\"\r\n        },\r\n        {\r\n            \"country\": \"USA\",\r\n            \"description\": \"Toy Hunter follows toy and collectibles expert and dealer Jordan Hembrough as he scours the U.S. for hidden treasures to sell to buyers around the world. In each episode, he travels from city to city, strategically manoeuvring around reluctant sellers, abating budgets, and avoiding unforeseen roadblocks.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 2,\r\n            \"genre\": \"Reality\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/ToyHunter1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": null,\r\n            \"primaryColour\": \"#0084da\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/toyhunter/season/1\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/toyhunter\",\r\n            \"title\": \"Toy Hunter\",\r\n            \"tvChannel\": \"GO!\"\r\n        },\r\n        {\r\n            \"country\": \"AUS\",\r\n            \"description\": \"A series of documentary specials featuring some of the world's most frightening moments, greatest daredevils and craziest weddings.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 1,\r\n            \"genre\": \"Documentary\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/Worlds1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": null,\r\n            \"primaryColour\": \"#ff7800\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/worlds/season/1\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/worlds\",\r\n            \"title\": \"World's...\",\r\n            \"tvChannel\": \"Channel 9\"\r\n        },\r\n        {\r\n            \"country\": \"USA\",\r\n            \"description\": \"Another year of bachelorhood brought many new adventures for roommates Walden Schmidt and Alan Harper. After his girlfriend turned down his marriage proposal, Walden was thrown back into the dating world in a serious way. The guys may have thought things were going to slow down once Jake got transferred to Japan, but they're about to be proven wrong when a niece of Alan's, who shares more than a few characteristics with her father, shows up at the beach house.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 0,\r\n            \"genre\": \"Comedy\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/TwoandahHalfMen_V2.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": {\r\n                \"channel\": null,\r\n                \"channelLogo\": \"http://catchup.ninemsn.com.au/img/player/Ch9_new_logo.gif\",\r\n                \"date\": null,\r\n                \"html\": \"Next episode airs: <span> 10:00pm Monday on<br><span class=\\\"visit\\\">Visit the Official Website</span></span>\",\r\n                \"url\": \"http://channelnine.ninemsn.com.au/twoandahalfmen/\"\r\n            },\r\n            \"primaryColour\": \"#ff7800\",\r\n            \"seasons\": null,\r\n            \"slug\": \"show/twoandahalfmen\",\r\n            \"title\": \"Two and a Half Men\",\r\n            \"tvChannel\": \"Channel 9\"\r\n        },\r\n        {\r\n            \"country\": \"USA\",\r\n            \"description\": \"Simmering with supernatural elements and featuring familiar and fan-favourite characters from the immensely popular drama The Vampire Diaries, it's The Originals. This sexy new series centres on the Original vampire family and the dangerous vampire/werewolf hybrid, Klaus, who returns to the magical melting pot that is the French Quarter of New Orleans, a town he helped build centuries ago.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 1,\r\n            \"genre\": \"Action\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/TheOriginals1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": {\r\n                \"channel\": null,\r\n                \"channelLogo\": \"http://catchup.ninemsn.com.au/img/player/logo_go.gif\",\r\n                \"date\": null,\r\n                \"html\": \"<br><span class=\\\"visit\\\">Visit the Official Website</span></span>\",\r\n                \"url\": \"http://go.ninemsn.com.au/\"\r\n            },\r\n            \"primaryColour\": \"#df0000\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/theoriginals/season/1\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/theoriginals\",\r\n            \"title\": \"The Originals\",\r\n            \"tvChannel\": \"GO!\"\r\n        },\r\n        {\r\n            \"country\": \"AUS\",\r\n            \"description\": \"Join the most dynamic TV judging panel Australia has ever seen as they uncover the next breed of superstars every Sunday night. UK comedy royalty Dawn French, international pop superstar Geri Halliwell, (in) famous Aussie straight-talking radio jock Kyle Sandilands, and chart -topping former AGT alumni Timomatic.\",\r\n            \"drm\": false,\r\n            \"episodeCount\": 0,\r\n            \"genre\": \"Reality\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/AGT.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": {\r\n                \"channel\": null,\r\n                \"channelLogo\": \"http://catchup.ninemsn.com.au/img/player/Ch9_new_logo.gif\",\r\n                \"date\": null,\r\n                \"html\": \"Next episode airs:<span>6:30pm Sunday on<br><span class=\\\"visit\\\">Visit the Official Website</span></span>\",\r\n                \"url\": \"http://agt.ninemsn.com.au\"\r\n            },\r\n            \"primaryColour\": \"#df0000\",\r\n            \"seasons\": null,\r\n            \"slug\": \"show/australiasgottalent\",\r\n            \"title\": \"Australia's Got Talent\",\r\n            \"tvChannel\": \"Channel 9\"\r\n        }\r\n    ],\r\n    \"skip\": 0,\r\n    \"take\": 10,\r\n    \"totalRecords\": 75\r\n}";
        private string invalidRequestJson = "{\r\n    \"payload\"        \"drm\": true,\r\n            \"episodeCount\": 3,\r\n            \"genre\": \"Reality\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/16KidsandCounting1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": null,\r\n            \"primaryColour\": \"#ff7800\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/16kidsandcounting/season/1\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/16kidsandcounting\",\r\n            \"title\": \"16 Kids and Counting\",\r\n            \"tvChannel\": \"GEM\"\r\n        },\r\n        {\r\n            \"slug\": \"show/seapatrol\",\r\n            \"title\": \"Sea Patrol\",\r\n            \"tvChannel\": \"Channel 9\"\r\n        },\r\n        {\r\n            \"country\": \" USA\",\r\n            \"description\": \"The Taste puts 16 culinary competitors in the kitchen, where four of the World's most notable culinary masters of the food world judges their creations based on a blind taste. Join judges Anthony Bourdain, Nigella Lawson, Ludovic Lefebvre and Brian Malarkey in this pressure-packed contest where a single spoonful can catapult a contender to the top or send them packing.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 2,\r\n            \"genre\": \"Reality\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/TheTaste1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": {\r\n                \"channel\": null,\r\n                \"channelLogo\": \"http://catchup.ninemsn.com.au/img/player/logo_go.gif\",\r\n                \"date\": null,\r\n                \"html\": \"<br><span class=\\\"visit\\\">Visit the Official Website</span></span>\",\r\n                \"url\": \"http://go.ninemsn.com.au/\"\r\n            },\r\n            \"primaryColour\": \"#df0000\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/thetaste/season/1\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/thetaste\",\r\n            \"title\": \"The Taste\",\r\n            \"tvChannel\": \"GEM\"\r\n        },\r\n        {\r\n            \"country\": \"UK\",\r\n            \"description\": \"The series follows the adventures of International Rescue, an organisation created to help those in grave danger using technically advanced equipment and machinery. The series focuses on the head of the organisation, ex-astronaut Jeff Tracy, and his five sons who piloted the \\\"Thunderbird\\\" machines.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 24,\r\n            \"genre\": \"Action\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/Thunderbirds_1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": null,\r\n            \"primaryColour\": \"#0084da\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/thunderbirds/season/1\"\r\n                },\r\n                {\r\n                    \"slug\": \"show/thunderbirds/season/3\"\r\n                },\r\n                {\r\n                    \"slug\": \"show/thunderbirds/season/4\"\r\n                },\r\n                {\r\n                    \"slug\": \"show/thunderbirds/season/5\"\r\n                },\r\n                {\r\n                    \"slug\": \"show/thunderbirds/season/6\"\r\n                },\r\n                {\r\n                    \"slug\": \"show/thunderbirds/season/8\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/thunderbirds\",\r\n            \"title\": \"Thunderbirds\",\r\n            \"tvChannel\": \"Channel 9\"\r\n        },\r\n        {\r\n            \"country\": \"USA\",\r\n            \"description\": \"A sleepy little village, Crystal Cove boasts a long history of ghost sightings, poltergeists, demon possessions, phantoms and other paranormal occurrences. The renowned sleuthing team of Fred, Daphne, Velma, Shaggy and Scooby-Doo prove all of this simply isn't real, and along the way, uncover a larger, season-long mystery that will change everything.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 4,\r\n            \"genre\": \"Kids\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/ScoobyDoo1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": null,\r\n            \"primaryColour\": \"#1b9e00\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/scoobydoomysteryincorporated/season/1\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/scoobydoomysteryincorporated\",\r\n            \"title\": \"Scooby-Doo! Mystery Incorporated\",\r\n            \"tvChannel\": \"GO!\"\r\n        },\r\n        {\r\n            \"country\": \"USA\",\r\n            \"description\": \"Toy Hunter follows toy and collectibles expert and dealer Jordan Hembrough as he scours the U.S. for hidden treasures to sell to buyers around the world. In each episode, he travels from city to city, strategically manoeuvring around reluctant sellers, abating budgets, and avoiding unforeseen roadblocks.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 2,\r\n            \"genre\": \"Reality\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/ToyHunter1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": null,\r\n            \"primaryColour\": \"#0084da\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/toyhunter/season/1\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/toyhunter\",\r\n            \"title\": \"Toy Hunter\",\r\n            \"tvChannel\": \"GO!\"\r\n        },\r\n        {\r\n            \"country\": \"AUS\",\r\n            \"description\": \"A series of documentary specials featuring some of the world's most frightening moments, greatest daredevils and craziest weddings.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 1,\r\n            \"genre\": \"Documentary\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/Worlds1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": null,\r\n            \"primaryColour\": \"#ff7800\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/worlds/season/1\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/worlds\",\r\n            \"title\": \"World's...\",\r\n            \"tvChannel\": \"Channel 9\"\r\n        },\r\n        {\r\n            \"country\": \"USA\",\r\n            \"description\": \"Another year of bachelorhood brought many new adventures for roommates Walden Schmidt and Alan Harper. After his girlfriend turned down his marriage proposal, Walden was thrown back into the dating world in a serious way. The guys may have thought things were going to slow down once Jake got transferred to Japan, but they're about to be proven wrong when a niece of Alan's, who shares more than a few characteristics with her father, shows up at the beach house.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 0,\r\n            \"genre\": \"Comedy\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/TwoandahHalfMen_V2.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": {\r\n                \"channel\": null,\r\n                \"channelLogo\": \"http://catchup.ninemsn.com.au/img/player/Ch9_new_logo.gif\",\r\n                \"date\": null,\r\n                \"html\": \"Next episode airs: <span> 10:00pm Monday on<br><span class=\\\"visit\\\">Visit the Official Website</span></span>\",\r\n                \"url\": \"http://channelnine.ninemsn.com.au/twoandahalfmen/\"\r\n            },\r\n            \"primaryColour\": \"#ff7800\",\r\n            \"seasons\": null,\r\n            \"slug\": \"show/twoandahalfmen\",\r\n            \"title\": \"Two and a Half Men\",\r\n            \"tvChannel\": \"Channel 9\"\r\n        },\r\n        {\r\n            \"country\": \"USA\",\r\n            \"description\": \"Simmering with supernatural elements and featuring familiar and fan-favourite characters from the immensely popular drama The Vampire Diaries, it's The Originals. This sexy new series centres on the Original vampire family and the dangerous vampire/werewolf hybrid, Klaus, who returns to the magical melting pot that is the French Quarter of New Orleans, a town he helped build centuries ago.\",\r\n            \"drm\": true,\r\n            \"episodeCount\": 1,\r\n            \"genre\": \"Action\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/TheOriginals1280.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": {\r\n                \"channel\": null,\r\n                \"channelLogo\": \"http://catchup.ninemsn.com.au/img/player/logo_go.gif\",\r\n                \"date\": null,\r\n                \"html\": \"<br><span class=\\\"visit\\\">Visit the Official Website</span></span>\",\r\n                \"url\": \"http://go.ninemsn.com.au/\"\r\n            },\r\n            \"primaryColour\": \"#df0000\",\r\n            \"seasons\": [\r\n                {\r\n                    \"slug\": \"show/theoriginals/season/1\"\r\n                }\r\n            ],\r\n            \"slug\": \"show/theoriginals\",\r\n            \"title\": \"The Originals\",\r\n            \"tvChannel\": \"GO!\"\r\n        },\r\n        {\r\n            \"country\": \"AUS\",\r\n            \"description\": \"Join the most dynamic TV judging panel Australia has ever seen as they uncover the next breed of superstars every Sunday night. UK comedy royalty Dawn French, international pop superstar Geri Halliwell, (in) famous Aussie straight-talking radio jock Kyle Sandilands, and chart -topping former AGT alumni Timomatic.\",\r\n            \"drm\": false,\r\n            \"episodeCount\": 0,\r\n            \"genre\": \"Reality\",\r\n            \"image\": {\r\n                \"showImage\": \"http://catchup.ninemsn.com.au/img/jump-in/shows/AGT.jpg\"\r\n            },\r\n            \"language\": \"English\",\r\n            \"nextEpisode\": {\r\n                \"channel\": null,\r\n                \"channelLogo\": \"http://catchup.ninemsn.com.au/img/player/Ch9_new_logo.gif\",\r\n                \"date\": null,\r\n                \"html\": \"Next episode airs:<span>6:30pm Sunday on<br><span class=\\\"visit\\\">Visit the Official Website</span></span>\",\r\n                \"url\": \"http://agt.ninemsn.com.au\"\r\n            },\r\n            \"primaryColour\": \"#df0000\",\r\n            \"seasons\": null,\r\n            \"slug\": \"show/australiasgottalent\",\r\n            \"title\": \"Australia's Got Talent\",\r\n            \"tvChannel\": \"Channel 9\"\r\n        }\r\n    ],\r\n    \"skip\": 0,\r\n    \"take\": 10,\r\n    \"totalRecords\": 75\r\n}";
        private string validResponseJson = "{\"response\":[{\"image\":\"http://catchup.ninemsn.com.au/img/jump-in/shows/16KidsandCounting1280.jpg\",\"slug\":\"show/16kidsandcounting\",\"title\":\"16 Kids and Counting\"},{\"image\":\"http://catchup.ninemsn.com.au/img/jump-in/shows/TheTaste1280.jpg\",\"slug\":\"show/thetaste\",\"title\":\"The Taste\"},{\"image\":\"http://catchup.ninemsn.com.au/img/jump-in/shows/Thunderbirds_1280.jpg\",\"slug\":\"show/thunderbirds\",\"title\":\"Thunderbirds\"},{\"image\":\"http://catchup.ninemsn.com.au/img/jump-in/shows/ScoobyDoo1280.jpg\",\"slug\":\"show/scoobydoomysteryincorporated\",\"title\":\"Scooby-Doo! Mystery Incorporated\"},{\"image\":\"http://catchup.ninemsn.com.au/img/jump-in/shows/ToyHunter1280.jpg\",\"slug\":\"show/toyhunter\",\"title\":\"Toy Hunter\"},{\"image\":\"http://catchup.ninemsn.com.au/img/jump-in/shows/Worlds1280.jpg\",\"slug\":\"show/worlds\",\"title\":\"World's...\"},{\"image\":\"http://catchup.ninemsn.com.au/img/jump-in/shows/TheOriginals1280.jpg\",\"slug\":\"show/theoriginals\",\"title\":\"The Originals\"}]}";
        private string errorJson = "{\"error\":\"Could not decode request: JSON parsing failed\"}";

        [TestMethod]
        public async Task PostData_SendValidJson_StatusCodeOK()
        {
            try
            {
                var config = new HttpConfiguration();
                var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
                var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "RequestData" } });

                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/");                
                request.Content = new StringContent(validRequestJson, Encoding.UTF8, "application/json");
                request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                RequestDataController controller = new RequestDataController();
                
                controller.ControllerContext = new HttpControllerContext(config, routeData, request);
                controller.Request = request;
                controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

                // Act
                var response = await controller.PostData(request);
                //var jsonString = await response.Content.ReadAsStringAsync();
                
                // Assert
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task PostData_SendInvalidJson_BadRequest()
        {
            try
            {
                var config = new HttpConfiguration();
                var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
                var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "RequestData" } });

                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/");
                request.Content = new StringContent(invalidRequestJson, Encoding.UTF8, "application/json");
                request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                RequestDataController controller = new RequestDataController();

                controller.ControllerContext = new HttpControllerContext(config, routeData, request);
                controller.Request = request;
                controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

                // Act
                var response = await controller.PostData(request);
                //var jsonString = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task PostData_SendValidJson_ValidResponseJson()
        {
            try
            {
                var config = new HttpConfiguration();
                var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
                var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "RequestData" } });

                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/");
                request.Content = new StringContent(validRequestJson, Encoding.UTF8, "application/json");
                request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                RequestDataController controller = new RequestDataController();

                controller.ControllerContext = new HttpControllerContext(config, routeData, request);
                controller.Request = request;
                controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

                // Act
                var response = await controller.PostData(request);
                var jsonString = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(validResponseJson, jsonString);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task PostData_SendInvalidJson_ErrorJson()
        {
            try
            {
                var config = new HttpConfiguration();
                var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
                var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "RequestData" } });

                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/");
                request.Content = new StringContent(invalidRequestJson, Encoding.UTF8, "application/json");
                request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                RequestDataController controller = new RequestDataController();

                controller.ControllerContext = new HttpControllerContext(config, routeData, request);
                controller.Request = request;
                controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

                // Act
                var response = await controller.PostData(request);
                var jsonString = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(errorJson, jsonString);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }
        }
    }
}
