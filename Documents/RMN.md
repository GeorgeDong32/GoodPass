# Repository Manager Note

> 仓库管理笔记

用于记录有关仓库管理的相关事项，便于标准化仓库分支和提交，供GeorgeDong32使用，当然你也可以参考

## Branch Management(分支管理)

### main

用于存放项目的主要介绍文件，如`README.md`，`支持文档`，`帮助文档` 等

### GoodPass-v2

GoodPass v2 CLI 版本开源代码存放分支，用于存放release的对应代码，供审查使用

### GoodPass-v3

GoodPass v3 GUI 版本开源代码存放分支，用于存放release的对应代码，供审查使用

### resource(已弃用，并入main分支resource文件夹)

用于存放项目资源的分支，包括但不限于`Icon`, `README附图`

### Developer&its derived branches（已弃用）

#### Developer branch

用于存放开发代码的仓库，用于开发备份和回滚

#### derived branches

通常为 `DEV-v+版本号` ，用于进行开发，开发完成后将并入 `Developer` 分支，并保留至下一个版本开发完成

### Open-source（已弃用，并入main分支Open source文件夹）

开源代码分支，供外界审查

## Commits Management(提交管理)

### main

提交时记为 `update filename` 或 `改动概述（如修改文件内结构） ` 

### GoodPass-v2

提交时标记为 `Update to v{版本号}`

### GoodPass-v3

提交时标记为 `Update to v{版本号}`

### resource（弃用）

提交时记为 `update filename` 或 `create filename` 

### Developer&its derived branches（弃用）

#### Developer branch

通常不直接提交代码

#### derived branches

* 初始化分支后, 修改文件版本号为dev -- `launch 版本号 development`
* 开发过程中, 提交按顺序记为 -- `Update 版本号 dev(序号)`

  * 例：
  * ```
    2.5.0 开发第一版
    Update 2.5.0 dev1 build22604.14
    
    2.5.0 开发第二版
    Update 2.5.0 dev2 build22614.22
    ```
  * 其中 `build` 部分号数为 `年份后两位 + 月份 + 日期 + . + 小时` ，月份超过9时待定

* 版本结束, 修改文件版本号为普通版本 -- `finish 版本号 development`
* Pull Requests -- `PR of 版本号`

### Open-source（弃用）

提交开源时 -- `open source 版本号 of 大版本(vn.0)`

文件夹命名 -- `Open source 大版本(vn.0)`
