# 《ToolGood 内容审核系统》

官网：https://toolgood.com/


《ToolGood 内容审核系统》由多组高性能算法组成：
- （1）在`ToolGood.Words`高性能算法基础上再次改进，十几处修改，性能更高；
- （2）优化`繁体与简体`、`全角半角`、`英文大小写`匹配性能；
- （3）改良算法，在`不减性能`情况下，压缩字典量，减少内存使用量；
- （4）加入`多组敏感词检测`，减少误杀；
- （5）加入`NLP分词功能`减少误杀概率，NLP算法使用`动态规划`，功能增加性能不降多少；
- （6）改良`联系方式匹配`，减少跳词的误测； 

## 文件夹说明
- `src`文件夹:  ToolGood.TextFilter源码，`C#`语言。
- `api`文件夹:  API接口源码，目前有`C#`、`JAVA`、`Python`、`GO`版本。
- `manager`文件夹: 敏感词库管理工具源码，(开发中)

注：`C#`语言编译环境`VS2019`，核心源码只有`C#`语言版本，其他语言本人并不精通。

## 相关下载
- 编译后程序下载（非异步版）： https://pan.baidu.com/s/1FLH7U3Nw2zE0Q3Vb-GtWZg 提取码：bqgn 
- 测试数据包（只有谩骂识别 ）：https://pan.baidu.com/s/12JjvSG1lAifdpRHH1GL1gA 提取码：tofy
- 相关文档：https://toolgood.com/FAQ


## 领取【敏感词库】
- 该敏感词库从`70W多词组`中整理出来的，外加18W多组常用分词、2W多字的拼音扩展、14066个左右拆字、4000组繁简转化，4850组同音同形字，1160组异形字…… 
- 免费领取方法： 加QQ`1665690808`，发送申请人照片，要求手执【免费领取敏感词库】纸条，并且站在带公司Logo的墙前（或学校大门口）。
-
- **如不想拍照，可花100元购买【敏感词库】，并赠送【程序数据包】**。
-
- 为什么会有免费领取【敏感词库】？因为一套敏感词库无法满足各种场景，而每个公司使用的场景又不同。如`你妈`，在熟人环境下是`正常词`，在商品评价绝对是`脏词`，在游戏中绝大数为`脏词`。

## 特别声明
- 源码**不包含**`生成程序数据包`功能，如有需要，请联系QQ`1665690808`。
- 人工服务：`生成敏感词数据包`价格100元一次，出错可尝试三次。 
- 购买`程序数据包生成器`价格1000元，windows版本，无时间限制。
- 购买`程序数据包生成器`**源码**价格5000元，`C#`语言。

### 提示

使用公开的源码可以反推数据包生成算法，需要以下能力：

- **精通**`C#`语言。
- **精通**`AC自动机`算法和`ToolGood.Words`算法。
- **精通**`正则表达式`，并能**手写**`正则表达式`解析过程。
- **精通**`算法优化`、`结构优化`。
- **掌握**`文件压缩原理`。

并且需要一个月以上时间进行开发、测试。

### 探讨敏感信息过滤：

敏感信息过滤研究会，Q群：128994346

## LICENSE
>您可以在GPLv3许可证下使用它。请参阅LICENSE。