﻿/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ToolGood.TextFilter
{
    public static partial class HtmlDecodeSearch
    {
        private const char HIGH_SURROGATE_START = '\uD800';
        private const char LOW_SURROGATE_START = '\uDC00';
        private const char LOW_SURROGATE_END = '\uDFFF';
        private const int UNICODE_PLANE00_END = 0x00FFFF;
        private const int UNICODE_PLANE01_START = 0x10000;
        private const int UNICODE_PLANE16_END = 0x10FFFF;


        static ushort[] _dict;
        static ushort[] _key;
        static int[] _next;
        static ushort[] _char;

        static HtmlDecodeSearch()
        {
            ushort[] _dict1 = new ushort[] { 35, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 59, 65, 66, 67, 68, 69, 70, 71, 72, 73, 75, 76, 77, 78, 79, 80, 82, 83, 84, 85, 88, 89, 90, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122 };
            ushort[] _dict2 = new ushort[] { 1, 16, 32, 40, 33, 41, 16, 16, 16, 16, 16, 55, 18, 19, 20, 21, 22, 15, 2, 17, 3, 4, 5, 6, 34, 23, 7, 28, 8, 14, 9, 10, 11, 12, 42, 29, 35, 36, 43, 44, 37, 45, 38, 27, 13, 46, 54, 52, 47, 48, 30, 49, 50, 51, 39, 31, 24, 25, 53, 26 };
            _key = new ushort[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 0, 0, 0, 18, 19, 20, 21, 22, 23, 24, 25, 26, 17, 28, 29, 30, 24, 23, 26, 34, 35, 36, 37, 38, 39, 32, 33, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 36, 37, 35, 38, 39, 38, 40, 41, 30, 30, 37, 31, 25, 39, 18, 42, 42, 24, 42, 43, 42, 42, 42, 15, 16, 27, 18, 19, 20, 21, 22, 36, 18, 38, 39, 17, 43, 42, 42, 43, 32, 33, 38, 42, 45, 46, 45, 43, 40, 41, 42, 39, 38, 38, 42, 43, 42, 43, 43, 35, 42, 42, 37, 45, 38, 46, 36, 38, 37, 45, 35, 46, 43, 47, 0, 35, 47, 37, 48, 39, 49, 35, 42, 37, 49, 39, 26, 47, 42, 29, 48, 49, 43, 46, 48, 35, 36, 48, 38, 39, 28, 54, 38, 43, 36, 45, 42, 39, 48, 45, 35, 43, 35, 49, 36, 38, 39, 49, 18, 42, 43, 38, 45, 46, 47, 50, 49, 48, 45, 50, 52, 42, 49, 50, 44, 35, 36, 39, 0, 14, 51, 51, 42, 43, 44, 45, 46, 38, 22, 51, 50, 42, 43, 48, 45, 46, 47, 52, 49, 50, 35, 35, 37, 37, 39, 39, 18, 42, 42, 42, 43, 27, 46, 48, 43, 49, 51, 51, 51, 49, 47, 51, 49, 35, 36, 52, 55, 35, 36, 36, 42, 43, 44, 29, 29, 47, 52, 49, 50, 51, 36, 30, 38, 39, 52, 37, 42, 43, 55, 18, 30, 47, 49, 48, 50, 51, 36, 54, 48, 54, 49, 44, 54, 52, 54, 54, 35, 46, 37, 49, 39, 54, 52, 42, 22, 54, 29, 50, 29, 48, 29, 38, 55, 29, 29, 54, 29, 35, 30, 37, 31, 39, 30, 35, 42, 37, 30, 39, 55, 25, 42, 43, 50, 51, 30, 47, 54, 44, 50, 35, 52, 37, 54, 39, 55, 30, 42, 30, 55, 30, 30, 0, 48, 0, 31, 51, 52, 35, 54, 37, 31, 39, 31, 31, 42, 43, 31, 31, 46, 31, 48, 49, 50, 51, 31, 35, 54, 37, 31, 39, 16, 31, 42, 43, 16, 31, 46, 31, 48, 49, 50, 51, 52, 25, 54, 34, 32, 33, 35, 0, 32, 33, 32, 33, 40, 41, 35, 35, 40, 41, 40, 15, 16, 43, 18, 19, 20, 21, 22, 55, 35, 35, 35, 55, 35, 55, 35, 35, 32, 33, 35, 0, 35, 35, 35, 35, 40, 41, 42, 15, 16, 35, 18, 19, 20, 21, 22, 35, 35, 35, 35, 55, 35, 35, 35, 35, 32, 33, 35, 0, 35, 35, 35, 35, 40, 41, 42, 15, 16, 36, 18, 19, 20, 21, 22, 36, 36, 36, 36, 55, 36, 36, 36, 36, 32, 33, 36, 36, 0, 36, 36, 36, 40, 41, 42, 15, 16, 36, 18, 19, 20, 21, 22, 36, 0, 37, 37, 55, 37, 37, 37, 37, 32, 33, 37, 37, 37, 0, 37, 37, 40, 41, 42, 15, 16, 37, 18, 19, 20, 21, 22, 37, 37, 37, 37, 55, 37, 0, 38, 38, 32, 33, 38, 38, 38, 38, 0, 38, 40, 41, 42, 15, 16, 38, 18, 19, 20, 21, 22, 38, 38, 38, 38, 55, 38, 38, 38, 38, 32, 33, 38, 38, 38, 38, 0, 38, 40, 41, 42, 15, 16, 38, 18, 19, 20, 21, 22, 38, 38, 38, 38, 55, 38, 38, 38, 38, 32, 33, 38, 38, 38, 38, 0, 38, 40, 41, 42, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 55, 38, 38, 38, 0, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 0, 0, 41, 41, 0, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 0, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 0, 44, 44, 0, 45, 45, 45, 45, 45, 45, 45, 0, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 0, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 0, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 0, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 0, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 0, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 51, 0, 52, 52, 52, 52, 52, 52, 52, 52, 52, 52, 52, 52, 52, 52, 52, 52, 52, 52, 52, 52, 0, 53, 53, 53, 53, 53, 53, 0, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 54, 0, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55, 55 };
            _next = new int[] { 0, 353, 606, 95, 607, 608, 570, 138, 22, 101, 485, 29, 658, 609, 73, 0, 0, 0, 181, 659, 79, 31, 180, 267, 660, 486, 48, 1184, 713, 123, 571, 621, 127, 1066, 153, 132, 69, 182, 273, 246, 21, 605, 325, 289, 188, 51, 203, 307, 164, 155, 115, 119, 219, 67, 50, 1100, 1101, 651, 838, 906, 484, 1083, 1084, 577, 585, 1026, 540, 385, 972, 829, 725, 408, 199, 479, 758, 300, 304, 968, 381, 381, 1166, 381, 381, 381, 381, 381, 610, 846, 207, 980, 9, 915, 348, 830, 74, 381, 381, 372, 1089, 1115, 790, 1090, 456, 381, 381, 381, 953, 35, 399, 356, 927, 86, 80, 467, 700, 600, 831, 1137, 704, 957, 734, 1036, 929, 464, 557, 592, 911, 185, 85, 0, 569, 814, 867, 1037, 977, 847, 563, 412, 864, 841, 973, 723, 925, 410, 277, 1152, 853, 712, 426, 893, 615, 769, 717, 223, 220, 346, 665, 102, 357, 305, 948, 575, 750, 616, 84, 8, 914, 690, 613, 542, 850, 97, 323, 816, 805, 133, 1213, 541, 595, 160, 735, 634, 902, 565, 801, 912, 1044, 567, 568, 539, 668, 281, 1156, 0, 10, 1045, 1124, 235, 28, 724, 772, 956, 274, 760, 546, 33, 820, 821, 952, 494, 581, 582, 466, 81, 500, 555, 559, 859, 862, 970, 971, 825, 402, 404, 966, 1134, 1165, 807, 892, 1014, 560, 641, 561, 1135, 664, 834, 614, 623, 675, 302, 786, 0, 813, 418, 232, 226, 1061, 732, 871, 186, 7, 53, 960, 34, 1062, 611, 587, 989, 990, 572, 955, 262, 991, 0, 839, 579, 896, 788, 355, 574, 489, 491, 739, 1042, 1009, 827, 1052, 1015, 458, 506, 266, 534, 430, 840, 818, 964, 1053, 454, 90, 752, 670, 722, 947, 993, 887, 431, 928, 0, 890, 635, 743, 452, 548, 593, 856, 1046, 969, 576, 536, 398, 844, 580, 965, 0, 514, 384, 42, 753, 550, 589, 908, 70, 1016, 537, 517, 145, 835, 629, 218, 0, 578, 376, 588, 0, 586, 596, 0, 882, 0, 674, 56, 883, 507, 134, 822, 677, 959, 1097, 680, 371, 728, 683, 686, 508, 270, 729, 208, 730, 509, 696, 20, 59, 837, 698, 961, 349, 703, 380, 740, 349, 707, 104, 710, 789, 520, 950, 522, 19, 63, 803, 1200, 349, 349, 812, 0, 349, 349, 1010, 1011, 349, 349, 573, 897, 349, 349, 1012, 409, 409, 1013, 409, 409, 409, 409, 409, 0, 1030, 584, 823, 0, 1060, 0, 590, 1096, 409, 409, 591, 0, 1113, 1121, 594, 745, 409, 409, 409, 437, 437, 1132, 437, 437, 437, 437, 437, 1154, 597, 857, 1176, 0, 598, 1183, 599, 1196, 437, 437, 601, 0, 602, 1210, 603, 1224, 437, 437, 437, 465, 465, 767, 465, 465, 465, 465, 465, 662, 663, 666, 667, 0, 1039, 671, 622, 678, 465, 465, 689, 545, 0, 692, 694, 558, 465, 465, 465, 493, 493, 701, 493, 493, 493, 493, 493, 653, 0, 1020, 1028, 0, 618, 1056, 1070, 1104, 493, 493, 1108, 684, 688, 0, 1145, 638, 493, 493, 493, 521, 521, 1177, 521, 521, 521, 521, 521, 480, 699, 1191, 1197, 0, 974, 0, 1205, 1167, 521, 521, 721, 453, 370, 726, 0, 1041, 521, 521, 521, 549, 549, 1035, 549, 549, 549, 549, 549, 727, 824, 935, 236, 0, 457, 733, 800, 884, 549, 549, 836, 738, 316, 937, 0, 741, 549, 549, 549, 1226, 1226, 462, 1226, 1226, 1226, 1226, 1226, 842, 54, 845, 940, 0, 939, 435, 1148, 747, 1226, 1226, 748, 749, 693, 858, 0, 754, 1226, 1226, 1226, 473, 860, 756, 1187, 759, 863, 944, 761, 482, 865, 764, 1216, 0, 975, 1214, 868, 0, 1217, 791, 869, 895, 264, 1003, 771, 774, 773, 777, 778, 876, 932, 879, 899, 781, 780, 784, 785, 519, 903, 905, 907, 687, 909, 269, 792, 916, 917, 920, 1199, 923, 924, 926, 0, 0, 1001, 1082, 0, 978, 810, 976, 808, 870, 872, 995, 425, 242, 815, 427, 278, 1047, 878, 308, 880, 1069, 130, 736, 314, 1106, 317, 320, 463, 1127, 321, 1136, 1138, 851, 854, 1160, 1164, 1169, 891, 328, 1178, 332, 1185, 1189, 1192, 335, 1198, 1201, 1202, 339, 866, 1211, 1218, 1219, 1220, 341, 1225, 0, 922, 919, 547, 987, 992, 998, 604, 874, 1005, 875, 492, 1038, 877, 1050, 1055, 1057, 1059, 512, 1092, 1095, 1102, 1109, 1111, 1112, 1118, 1120, 1123, 1129, 1131, 888, 848, 1142, 436, 941, 1157, 855, 1171, 1173, 1175, 1180, 1182, 861, 440, 1190, 1193, 1195, 921, 1203, 1207, 1209, 1215, 1221, 1223, 0, 1080, 886, 0, 793, 985, 996, 1051, 626, 1172, 645, 0, 988, 424, 612, 490, 775, 1027, 1033, 1043, 511, 583, 619, 1058, 782, 1074, 513, 1081, 737, 1085, 787, 1093, 528, 432, 1110, 1114, 1119, 1128, 1130, 1150, 1155, 438, 751, 1161, 554, 639, 439, 1174, 795, 1181, 918, 1188, 562, 446, 1194, 1204, 796, 1208, 1222, 0, 894, 984, 898, 931, 1019, 1024, 1021, 1022, 776, 817, 1031, 1032, 934, 1063, 1064, 783, 826, 1072, 1073, 711, 936, 1094, 885, 938, 913, 1162, 1163, 942, 943, 945, 946, 0, 997, 1002, 1076, 1086, 1088, 1099, 1103, 1151, 1168, 719, 809, 648, 811, 654, 0, 981, 768, 930, 770, 819, 1025, 1029, 1034, 144, 225, 620, 779, 374, 828, 1068, 1071, 1075, 833, 832, 901, 1078, 1079, 624, 625, 377, 627, 382, 843, 628, 1117, 383, 1122, 631, 391, 849, 1139, 1140, 1143, 397, 852, 1144, 691, 1159, 806, 640, 794, 400, 642, 403, 1186, 646, 405, 650, 411, 797, 656, 413, 0, 982, 714, 798, 715, 799, 1004, 1006, 958, 1049, 716, 1054, 900, 1077, 518, 802, 904, 1105, 951, 535, 910, 1141, 1147, 718, 556, 564, 0, 983, 661, 258, 1008, 1017, 673, 1067, 881, 949, 676, 1098, 679, 681, 682, 630, 685, 1133, 633, 1149, 1158, 636, 637, 695, 697, 643, 644, 702, 647, 649, 705, 706, 655, 709, 0, 986, 994, 873, 999, 1018, 429, 1040, 1048, 672, 1091, 459, 1116, 1125, 1126, 1153, 1170, 1179, 481, 1206, 1212, 0, 954, 1007, 1087, 962, 963, 1146, 0, 720, 1000, 617, 1023, 669, 933, 731, 1065, 742, 804, 1107, 744, 746, 967, 632, 889, 755, 757, 762, 763, 765, 652, 708, 272, 766, 979, 657, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            _char = new ushort[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8834, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8744, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 952, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 172, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 960, 0, 0, 0, 963, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 2, 0, 8835, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 956, 175, 8722, 183, 181, 8212, 165, 253, 255, 8715, 957, 8800, 241, 8836, 8713, 8711, 8211, 160, 8482, 254, 977, 8756, 8201, 964, 215, 732, 9824, 173, 167, 8721, 185, 179, 178, 8839, 8838, 8764, 962, 8901, 353, 8218, 223, 8250, 8217, 8207, 961, 8971, 174, 8476, 9002, 8594, 8730, 187, 8221, 8969, 8658, 968, 8719, 8733, 8242, 163, 177, 966, 8869, 8240, 182, 8706, 982, 969, 959, 8855, 245, 248, 170, 186, 8853, 8254, 339, 243, 246, 242, 244, 8804, 60, 8249, 8216, 8206, 9674, 8727, 8970, 955, 9001, 8592, 171, 8220, 8968, 8656, 8230, 9829, 8596, 8660, 402, 8260, 190, 189, 188, 8704, 8195, 8709, 8194, 951, 240, 949, 233, 235, 8364, 232, 234, 8801, 8707, 38, 8743, 8736, 227, 8776, 229, 39, 945, 8501, 230, 225, 228, 224, 180, 226, 168, 978, 965, 8593, 250, 252, 249, 251, 8657, 8465, 8747, 8734, 8712, 953, 161, 237, 239, 236, 238, 191, 8805, 62, 947, 176, 948, 8595, 8224, 9830, 247, 8659, 8629, 8773, 169, 9827, 967, 162, 184, 8745, 8746, 164, 710, 231, 925, 209, 34, 166, 946, 8226, 8222, 929, 950, 8205, 8204, 958, 8472, 937, 927, 213, 216, 211, 214, 210, 212, 338, 919, 917, 201, 203, 200, 202, 208, 916, 8225, 935, 199, 914, 195, 197, 913, 193, 196, 192, 194, 198, 920, 932, 222, 954, 918, 221, 376, 926, 933, 218, 220, 217, 219, 931, 352, 928, 936, 8243, 934, 924, 923, 922, 921, 205, 207, 204, 206, 915, 1 };

            _dict = new ushort[char.MaxValue + 1];
            for (int i = 0; i < _dict1.Length; i++) {
                _dict[_dict1[i]] = _dict2[i];
            }
        }



        public static bool FindFirstChar(string text, ref int txtIdx, out char[] outChars)
        {
            var p = 0;
            uint parsedValue;
            for (int i = txtIdx+1; i < text.Length; i++) {
                var t1 = text[i];
                if (t1 > 128) { break; }
                var t = _dict[t1];
                if (t == 0) { break; }
                var next = _next[p] + t;
                bool find = _key[next] == t;
                if (find == false) { break; }
                var index = _char[next];
                if (index == 1 || index == 2) {
                    bool parsedSuccessfully = index == 1
                        ? uint.TryParse(text.Substring(txtIdx + 3, i - 3 - txtIdx), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out parsedValue)
                        : uint.TryParse(text.Substring(txtIdx + 2, i - 2 - txtIdx), NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedValue);
                    if (parsedSuccessfully) {
                        parsedSuccessfully = ((parsedValue < HIGH_SURROGATE_START) || (LOW_SURROGATE_END < parsedValue && parsedValue <= UNICODE_PLANE16_END));
                    }
                    if (parsedSuccessfully) {
                        if (parsedValue <= UNICODE_PLANE00_END) {
                            outChars = new char[] { (char)parsedValue };
                        } else {
                            ConvertSmpToUtf16(parsedValue, out char leadingSurrogate, out char trailingSurrogate);
                            outChars = new char[] { (char)leadingSurrogate, (char)trailingSurrogate };
                        }
                        txtIdx = i;
                        return true;
                    }
                    break;
                } else if (index > 2) {
                    txtIdx = i;
                    outChars = new char[] { (char)(ushort)index };
                    return true;
                }
                p = next;
            }
            //outChars = new char[] { text[txtIdx] };
            outChars = new char[0]  ;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConvertSmpToUtf16(uint smpChar, out char leadingSurrogate, out char trailingSurrogate)
        {
            int utf32 = (int)(smpChar - UNICODE_PLANE01_START);
            leadingSurrogate = (char)((utf32 / 0x400) + HIGH_SURROGATE_START);
            trailingSurrogate = (char)((utf32 % 0x400) + LOW_SURROGATE_START);
        }


 
 

    }
}
