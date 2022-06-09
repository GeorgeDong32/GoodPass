# Repository Manager Note

> 仓库管理笔记

用于记录有关仓库管理的相关事项，便于标准化仓库分支和提交，供GeorgeDong32使用，当然你也可以参考

## Branch Management(分支管理)

### main

用于存放项目的主要介绍文件，如`README.md`，`支持文档`，`帮助文档` 等

### resource

用于存放项目资源的分支，包括但不限于`Icon`, `README附图`

### Developer&its derived branches

#### Developer branch

用于存放开发代码的仓库，用于开发备份和回滚

#### derived branches

通常为 `DEV-v+版本号` ，用于进行开发，开发完成后将并入 `Developer` 分支，并保留至下一个版本开发完成

### Open-source

开源代码分支，供外界审查

## Commits Management(提交管理)

### main

提交时记为 `update filename` 或 `改动概述（如修改文件内结构） ` 

### resource

提交时记为 `update filename` 或 `create filename` 

### Developer&its derived branches

#### Developer branch

通常不直接提交代码

#### derived branches

* 初始化分支后, 修改文件版本号为dev -- `launch 版本号 development`
* 开发过程中, 提交按顺序记为 -- `update 版本号 dev(序号)`
* 版本结束, 修改文件版本号为普通版本 -- `finish 版本号 development`
* Pull Requests -- `PR of 版本号`

### Open-source

提交开源时 -- `open source 版本号 of 大版本(vn.0)`

文件夹命名 -- `Open source 大版本(vn.0)`
