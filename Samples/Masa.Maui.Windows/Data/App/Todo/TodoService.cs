namespace Masa.Maui.Data.App.Todo;

public class TodoService
{
    public static List<TodoDto> GetList() => new()
    {
        new TodoDto(1, false, false, false, false, "汉皇重色思倾国，御宇多年求不得。", "紫萱", 0, new DateOnly(2021, 9, 15), new List<string> { "Low", }, "1、最灵繁的人也看不见自己的背脊。——非洲"),
        new TodoDto(2, false, false, false, false, "杨家有女初长成，养在深闺人未识。", "若芹", 2, new DateOnly(2021, 9, 16), new List<string> { "Low" }, "2、最困难的事情就是认识自己。——希腊"),
        new TodoDto(3, true, false, true, false, "天生丽质难自弃，一朝选在君王侧。", "思菱", 5, new DateOnly(2021, 9, 17), new List<string> { "Low" }, "3、有勇气承担命运这才是英雄好汉。——黑塞"),
        new TodoDto(4, true, false, true, false, "回眸一笑百媚生，六宫粉黛无颜色。", "向秋", 3, new DateOnly(2021, 9, 18), new List<string> { "Low" }, "4、与肝胆人共事，无字句处读书。——周恩来"),
        new TodoDto(5, true, true, true, false, "春寒赐浴华清池，温泉水滑洗凝脂。", "雨珍", 4, new DateOnly(2021, 9, 19), new List<string> { "Low" }, "5、阅读使人充实，会谈使人敏捷，写作使人精确。——培根"),
        new TodoDto(6, false, true, false, false, "侍儿扶起娇无力，始是新承恩泽时。", "海瑶", 1, new DateOnly(2021, 9, 20), new List<string> { "High" }, "6、最大的骄傲于最大的自卑都表示心灵的最软弱无力。——斯宾诺莎"),
        new TodoDto(7, true, true, true, false, "云鬓花颜金步摇，芙蓉帐暖度春宵。", "乐萱", 2, new DateOnly(2021, 9, 21), new List<string> { "High" }, "7、自知之明是最难得的知识。——西班牙"),
        new TodoDto(8, true, true, false, false, "春宵苦短日高起，从此君王不早朝。", "紫萱", 5, new DateOnly(2021, 9, 22), new List<string> { "High" }, "8、勇气通往天堂，怯懦通往地狱。——塞内加"),
        new TodoDto(9, true, false, false, false, "承欢侍宴无闲暇，春从春游夜专夜。", "若芹", 3, new DateOnly(2021, 9, 23), new List<string> { "High" }, "9、有时候读书是一种巧妙地避开思考的方法。——赫尔普斯"),
        new TodoDto(10, true, false, false, false, "后宫佳丽三千人，三千宠爱在一身。", "思菱", 1, new DateOnly(2021, 9, 24), new List<string> { "High" }, "10、阅读一切好书如同和过去最杰出的人谈话。——笛卡儿"),
        new TodoDto(11, true, false, true, false, "金屋妆成娇侍夜，玉楼宴罢醉和春。", "向秋", 4, new DateOnly(2021, 9, 25), new List<string> { "High" }, "11、越是没有本领的就越加自命不凡。——邓拓"),
        new TodoDto(12, true, false, false, false, "姊妹弟兄皆列土，可怜光彩生门户。", "雨珍", 0, new DateOnly(2021, 9, 26), new List<string> { "High" }, "12、越是无能的人，越喜欢挑剔别人的错儿。——爱尔兰"),
        new TodoDto(13, true, true, false, false, "遂令天下父母心，不重生男重生女。", "海瑶", 5, new DateOnly(2021, 9, 27), new List<string> { "Medium" }, "13、知人者智，自知者明。胜人者有力，自胜者强。——老子"),
        new TodoDto(14, true, true, false, false, "骊宫高处入青云，仙乐风飘处处闻。", "乐萱", 4, new DateOnly(2021, 9, 28), new List<string> { "Medium" }, "14、意志坚强的人能把世界放在手中像泥块一样任意揉捏。——歌德"),
        new TodoDto(15, true, true, false, false, "缓歌慢舞凝丝竹，尽日君王看不足。", "紫萱", 1, new DateOnly(2021, 9, 29), new List<string> { "Medium" }, "15、最具挑战性的挑战莫过于提升自我。——迈克尔·F·斯特利"),
        new TodoDto(16, false, false, false, false, "渔阳鼙鼓动地来，惊破霓裳羽衣曲。", "若芹", 4, new DateOnly(2021, 9, 30), new List<string> { "Medium" }, "16、业余生活要有意义，不要越轨。——华盛顿"),
        new TodoDto(17, false, true, false, false, "九重城阙烟尘生，千乘万骑西南行。", "思菱", 4, new DateOnly(2021, 10, 1), new List<string> { "Medium" }, "17、一个人即使已登上顶峰，也仍要自强不息。——罗素·贝克"),
        new TodoDto(18, false, false, false, false, "翠华摇摇行复止，西出都门百余里。", "向秋", 4, new DateOnly(2021, 10, 2), new List<string> { "Medium" }, "18、最大的挑战和突破在于用人，而用人最大的突破在于信任人。——马云"),
        new TodoDto(19, false, true, false, false, "六军不发无奈何，宛转蛾眉马前死。", "雨珍", 0, new DateOnly(2021, 10, 3), new List<string> { "Medium" }, "19、自己活着，就是为了使别人过得更美好。——雷锋"),
        new TodoDto(20, false, false, false, false, "花钿委地无人收，翠翘金雀玉搔头。", "海瑶", 0, new DateOnly(2021, 10, 4), new List<string> { "Team" }, "20、要掌握书，莫被书掌握；要为生而读，莫为读而生。——布尔沃"),
        new TodoDto(21, false, false, false, false, "君王掩面救不得，回看血泪相和流。", "乐萱", 5, new DateOnly(2021, 10, 5), new List<string> { "Team" }, "21、要知道对好事的称颂过于夸大，也会招来人们的反感轻蔑和嫉妒。——培根"),
        new TodoDto(22, false, false, false, false, "黄埃散漫风萧索，云栈萦纡登剑阁。", "紫萱", 4, new DateOnly(2021, 10, 6), new List<string> { "Team" }, "22、业精于勤，荒于嬉；行成于思，毁于随。——韩愈"),
        new TodoDto(23, true, true, true, false, "峨嵋山下少人行，旌旗无光日色薄。", "若芹", 4, new DateOnly(2021, 10, 7), new List<string> { "Team" }, "23、一切节省，归根到底都归结为时间的节省。——马克思"),
        new TodoDto(24, false, false, false, false, "蜀江水碧蜀山青，圣主朝朝暮暮情。", "思菱", 4, new DateOnly(2021, 10, 8), new List<string> { "Team" }, "24、意志命运往往背道而驰，决心到最后会全部推倒。——莎士比亚"),
        new TodoDto(25, true, false, true, false, "行宫见月伤心色，夜雨闻铃肠断声。", "向秋", 1, new DateOnly(2021, 10, 9), new List<string> { "Team" }, "25、学习是劳动，是充满思想的劳动。——乌申斯基"),
        new TodoDto(26, false, true, false, true, "天旋地转回龙驭，到此踌躇不能去。", "雨珍", 1, new DateOnly(2021, 10, 10), new List<string> { "Team" }, "26、要使整个人生都过得舒适、愉快，这是不可能的，因为人类必须具备一种能应付逆境的态度。——卢梭"),
        new TodoDto(27, true, false, false, false, "马嵬坡下泥土中，不见玉颜空死处。", "海瑶", 2, new DateOnly(2021, 10, 11), new List<string> { "Team" }, "27、只有把抱怨环境的心情，化为上进的力量，才是成功的保证。——罗曼·罗兰"),
        new TodoDto(28, true, false, true, true, "君臣相顾尽沾衣，东望都门信马归。", "乐萱", 2, new DateOnly(2021, 10, 12), new List<string> { "Low" }, "28、知之者不如好之者，好之者不如乐之者。——孔子"),
        new TodoDto(29, true, true, false, false, "归来池苑皆依旧，太液芙蓉未央柳。", "紫萱", 1, new DateOnly(2021, 10, 13), new List<string> { "Low" }, "29、勇猛、大胆和坚定的决心能够抵得上武器的精良。——达·芬奇"),
        new TodoDto(30, true, false, false, false, "芙蓉如面柳如眉，对此如何不泪垂。", "若芹", 5, new DateOnly(2021, 10, 14), new List<string> { "Low" }, "30、意志是一个强壮的盲人，倚靠在明眼的跛子肩上。——叔本华")
    };

    public static List<SelectData> GetAssigneeList() => new()
    {
        new SelectData() { Label = "紫萱", Value = "紫萱" },
        new SelectData() { Label = "若芹", Value = "若芹" },
        new SelectData() { Label = "思菱", Value = "思菱" },
        new SelectData() { Label = "向秋", Value = "向秋" },
        new SelectData() { Label = "雨珍", Value = "雨珍" },
        new SelectData() { Label = "海瑶", Value = "海瑶" },
        new SelectData() { Label = "乐萱", Value = "乐萱" },
    };

    public static List<SelectData> GetTagList() => new()
    {
        new SelectData() { Label = "Team", Value = "Team" },
        new SelectData() { Label = "Low", Value = "Low" },
        new SelectData() { Label = "Medium", Value = "Medium" },
        new SelectData() { Label = "High", Value = "High" },
        new SelectData() { Label = "Update", Value = "Update" }
    };

    public static Dictionary<string, string> GetTagColorMap() => new()
    {
        { "Team", "pry" },
        { "Low", "sample-green" },
        { "Medium", "remind" },
        { "High", "error" },
        { "Update", "info" },
    };

    public static string[] GetAvatars() => new string[]
    {
        "/img/avatar/1.svg",
        "/img/avatar/8.svg",
        "/img/avatar/12.svg",
        "/img/avatar/10.svg",
        "/img/avatar/11.svg",
        "/img/avatar/9.svg"
    };
}
