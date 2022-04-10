#ifndef __GP_CODEBLOG_H__
#define __GP_CODEBLOG_H__
/*********************************************************
*                                                        *
* Blog.h -- Record the Update blogs                      *
* Copyright(c) GeorgeDong32(Github).All rights reserved. *
*                                                        *
**********************************************************/
//Update Blog
/*
* 2022.4.9
* 2.0.2
	  主密码校验算法更新，升级为SHA256校验
	  优化部分显示界面，便于阅读
	  新增彩色文字效果，使部分显示更直观
* 2022.4.8
* 2.0.1
	  更新了主密码校验算法，改为使用MD5校验，防止通过本地文件反推主密码，大幅提升安全性
* 2022.4.7
2.0.0(0407)
	  更新了软件图标，避免出现无法显示的情况
* 2022.4.5
2.0.0 全新的GoodPass 2.0.0
	  本地化密码管理，安全方便
	  全新的操作逻辑，简洁高效
	  主密码校验增强，提升保障
* 2022.4.5
2.0.0 pre
	  2.0.0版本前瞻版本，实现了本地化的密码管理
	  应用了全新的操作逻辑，更为人性化
	  优化了程序结构，提升效能
	  （主密码校验bug未完全解决，已进行加强）
* 2022.4.3
1.9.1 dev 
	  重写应用界面，更新操作逻辑
	  new bug：主密码校验不完全匹配也可进入程序，存在安全隐患
* 2022.3.31
1.9.0 (开发版本)
	  引入Data和Manager类，便于进行密码管理
* 2022.3.24
1.8.0 更改部分显示，为2.0统一风格
	  将打印类函数置于Display.h中，便于代码管理
	  *此版本为1.x最后一个可用版本
* 2022.3.22
1.7.5c 修正逻辑，使主密码校验顺利进行。同时删减余冗代码，优化空间占用。
* 2022.3.20
1.7.5 完成主密码部分，用主密码生成加密数组和验证身份
	  同时进行数据迁移
* 2022.3.18
1.7.3 引入主密码，增强安全性（未完成）
	  改变了文件写入模式，便于密码管理时读取
* 2022.3.16
1.7.2 重设图标，减小exe大小
* 2022.3.15
1.7.1 引入system函数设置背景和字体颜色，加入图标资源以及版本资源
* 2022.3.14
1.7.0 测试文件夹稳定性
* 2022.3.13
1.6.6 引入文件夹模式，便于后续管理
* 2022.3.10
1.6.5 重构了文件写入模块
* 2022.3.7 - 3.9
1.6.4 新增工程模式，便于调试
	  删减大量无效代码，提高可读性
* 2022.3.3-3.7
1.6.3 重构函数，使用string类代替字符数组
      优化函数，实现了多位大写和随机性
* 2022.3.1
1.6.2 更名为GoodPass,转用C++编写，提升程序拓展性
* 2022.2.26
1.6.1 完成了稳定性测试，可进入实用阶段；
	  调整了文件命名规则，增加文件可读性；
	  删除部分多余代码，提升程序性能。
* 2022.2.25
1.5.9 完成了程序的完善，优化了部分界面，实现了加密存储
* 2022.2.24
1.5.0 完成了主函数的重写，加密存储已成功，待大量测试
* 2022.2.23
1.4.5 完成了解密部分的所有代码
1.4.4 修改了解密部分的部分代码，提升了效率，节约了内存
* 2022.2.22
1.4.3 完成了密码解密部分的定位代码
1.4.2 修复1.4中没有清空并初始化最终数组导致的拼接问题
* Before 2022.2
1.4 新增结果存储部分，便于寻找生成过的密码，避免日志重复；结果存储部分加密算法完成，稳定性良好，加密效果良好
1.3 日志写入部分完成，稳定性良好，进入实际使用
1.2 加密算法完成
1.1 整体架构完成

//Todo List:

近期版本计划：
*2.0.3
1.将主密码校验升级为哈希值校验，提升安全性。去除当前校验方式，避免KEY泄露
*2.0.4
1.改进加密算法，使逻辑更难被解读，字母大小写顺序更不规律
*2.0.5
1.改进生成器机制，提高生成密码质量
未来计划：
*3.0
1.实现图形界面
*/

#endif