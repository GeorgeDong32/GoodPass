<h1 align=center>
    GoodPass 开源证书 1.0
</h1>
<p align=center>
    Copyright  (C) 2022-present  GeorgeDong32<br>
    证书基于 MPL 2.0, Apache License 2.0 以及 MIT License 修改而来
</p>
<h2 align=center>
    概览
</h2>
<table class="Overview">
    <div class="permisions" style="float:left;width:350px;">
        <h4 class="ptitle">许可</h4>
        <div class="plist">
            <div class="text-small pl-3">
                <svg width="13" class="checksign" viewBox="0 0 16 16" version="1.1" height="13" aria-hidden="true">
                    <path fill-rule="evenodd" d="M13.78 4.22a.75.75 0 010 1.06l-7.25 7.25a.75.75 0 01-1.06 0L2.22 9.28a.75.75 0 011.06-1.06L6 10.94l6.72-6.72a.75.75 0 011.06 0z"></path>
                </svg>
                <span class="v-align-middle" title="The licensed material may be used and modified in private.">
                    私人使用
                </span>
            </div>
            <div class="text-small pl-3">
                <svg width="13" class="checksign" viewBox="0 0 16 16" version="1.1" height="13" aria-hidden="true"><path fill-rule="evenodd" d="M13.78 4.22a.75.75 0 010 1.06l-7.25 7.25a.75.75 0 01-1.06 0L2.22 9.28a.75.75 0 011.06-1.06L6 10.94l6.72-6.72a.75.75 0 011.06 0z"></path></svg>
                <span class="v-align-middle" title="The licensed material may be modified.">
                    修改
                </span>
            </div>
            <div class="text-small pl-3">
                <svg width="13" class="checksign" viewBox="0 0 16 16" version="1.1" height="13" aria-hidden="true"><path fill-rule="evenodd" d="M13.78 4.22a.75.75 0 010 1.06l-7.25 7.25a.75.75 0 01-1.06 0L2.22 9.28a.75.75 0 011.06-1.06L6 10.94l6.72-6.72a.75.75 0 011.06 0z"></path></svg>
                <span class="v-align-middle" title="The licensed material may be distributed.">
                    分发
                </span>
            </div>
            <div class="text-small pl-3">
                <svg width="13" class="checksign" viewBox="0 0 16 16" version="1.1" height="13" aria-hidden="true"><path fill-rule="evenodd" d="M13.78 4.22a.75.75 0 010 1.06l-7.25 7.25a.75.75 0 01-1.06 0L2.22 9.28a.75.75 0 011.06-1.06L6 10.94l6.72-6.72a.75.75 0 011.06 0z"></path></svg>
                <span class="v-align-middle" title="The licensed material and derivatives may be used for commercial purposes under copyright owner's permission.">
                    受限的商业使用
                </span>
            </div>
        </div>
    </div>
        <div class="limitations" style="float:left;width:300px;">
          <h4 class="ltitle">限制</h4>
          <div class="llist">
              <div class="l1">
                <svg width="13" class="crosssign" viewBox="0 0 16 16" version="1.1" height="13" aria-hidden="true"><path fill-rule="evenodd" d="M3.72 3.72a.75.75 0 011.06 0L8 6.94l3.22-3.22a.75.75 0 111.06 1.06L9.06 8l3.22 3.22a.75.75 0 11-1.06 1.06L8 9.06l-3.22 3.22a.75.75 0 01-1.06-1.06L6.94 8 3.72 4.78a.75.75 0 010-1.06z"></path></svg>
                <span class="v-align-middle" title="This license includes a limitation of liability.">
                  责任
                </span>
              </div>
              <div class="l2">
                <svg width="13" class="crosssign" viewBox="0 0 16 16" version="1.1" height="13" aria-hidden="true"><path fill-rule="evenodd" d="M3.72 3.72a.75.75 0 011.06 0L8 6.94l3.22-3.22a.75.75 0 111.06 1.06L9.06 8l3.22 3.22a.75.75 0 11-1.06 1.06L8 9.06l-3.22 3.22a.75.75 0 01-1.06-1.06L6.94 8 3.72 4.78a.75.75 0 010-1.06z"></path></svg>
                <span class="v-align-middle" title="This license explicitly states that it does NOT provide any warranty.">
                  保证
                </span>
              </div>
              <div class="l3">
                <svg width="13" class="octicon octicon-x color-fg-danger ml-n3 v-align-middle" viewBox="0 0 16 16" version="1.1" height="13" aria-hidden="true"><path fill-rule="evenodd" d="M3.72 3.72a.75.75 0 011.06 0L8 6.94l3.22-3.22a.75.75 0 111.06 1.06L9.06 8l3.22 3.22a.75.75 0 11-1.06 1.06L8 9.06l-3.22 3.22a.75.75 0 01-1.06-1.06L6.94 8 3.72 4.78a.75.75 0 010-1.06z"></path></svg>
                <span class="v-align-middle" title="This license explicitly states that it does NOT grant trademark rights, even though licenses without such a statement probably do not grant any implicit trademark rights.">
                  商标使用
                </span>
              </div>
          </div>
        </div>
        <div class="conditions" style="float:left;width:300px;">
          <h4 class="ctitle">条件</h4>
            <dic class="clist">
                <div class="c1">
                    <svg width="13" class="octicon octicon-info color-fg-accent ml-n3 v-align-middle" viewBox="0 0 16 16" version="1.1" height="13" aria-hidden="true"><path fill-rule="evenodd" d="M8 1.5a6.5 6.5 0 100 13 6.5 6.5 0 000-13zM0 8a8 8 0 1116 0A8 8 0 010 8zm6.5-.25A.75.75 0 017.25 7h1a.75.75 0 01.75.75v2.75h.25a.75.75 0 010 1.5h-2a.75.75 0 010-1.5h.25v-2h-.25a.75.75 0 01-.75-.75zM8 6a1 1 0 100-2 1 1 0 000 2z"></path></svg>
                    <span class="v-align-middle" title="A copy of the license and copyright notice must be included with the licensed material.">
                        证书和版权声明
                    </span>
                </div>
                <div class="text-small pl-3">
                <svg width="13" class="octicon octicon-info color-fg-accent ml-n3 v-align-middle" viewBox="0 0 16 16" version="1.1" height="13" aria-hidden="true"><path fill-rule="evenodd" d="M8 1.5a6.5 6.5 0 100 13 6.5 6.5 0 000-13zM0 8a8 8 0 1116 0A8 8 0 010 8zm6.5-.25A.75.75 0 017.25 7h1a.75.75 0 01.75.75v2.75h.25a.75.75 0 010 1.5h-2a.75.75 0 010-1.5h.25v-2h-.25a.75.75 0 01-.75-.75zM8 6a1 1 0 100-2 1 1 0 000 2z"></path></svg>
                <span class="v-align-middle" title="Modifications must be released under the same license when distributing the licensed material. In some cases a similar or related license may be used.">
                  相同许可证
                </span>
              </div>
            </div>
        </div>
</table>



<h2 align=center>
    责任与保证
</h2>


```
	   涵盖软件根据本许可按“原样”提供，不提供任何形式的明示、暗示或法定保证，包括但不限于
	   对涵盖软件无缺陷、可销售、适合特定用途或非侵权的保证。有关涵盖软件的质量和性能的
	   全部风险由您承担。如果任何涵盖软件在任何方面被证明存在缺陷，您（而非任何贡献者）
	   承担任何必要的服务、维修或更正费用。本免责声明构成本许可的重要组成部分。除同意本
	   免责声明外，不允许使用任何涵盖软件。
```

```
       在任何情况下和任何法律理论下，无论是侵权（包括疏忽）、合同还是其他，任何贡献者或
       上述允许分发涵盖软件的任何人，均不对您承担任何性质的任何直接、间接、特殊、偶然或
       后果性损害，包括但不限于利润损失、商誉损失、停工、计算机故障或故障的损害， 或任何
       和所有其他商业损害或损失，即使该方已被告知此类损害的可能性。本责任限制不适用于因
       该方的疏忽而导致的死亡或人身伤害的责任，只要适用法律禁止此类限制。某些司法管辖区
       不允许排除或限制附带或后果性损害，因此此排除和限制可能不适用于您。                                     
```

<h2 align=center>
    授予的权利
</h2>


```
1.    授予版权许可。根据本许可的条款和条件，每个贡献者特此授予您永久的、全球性的、非排他性的、
	  免费的、免版税的、不可撤销的版权许可，以复制、准备衍生作品、公开展示、公开表演、再许可和
	  分发作品和此类衍生作品的源或对象形式。
```

```
2. 	  授予修改和私人使用。特此免费授予获得本软件和相关文档文件（“软件”）副本的任何人私人使用、
	  修改、合并和重新分发修改版本的权限。
```

<h2 align=center>
    再分发
</h2>


```
	  您可以以任何媒介（源或对象形式）复制和分发本作品或其衍生作品的副本，前提是您满足以下条件：

      （a） 您必须向本作品或衍生作品的任何其他接收者提供本许可的副本;

      （b） 您必须使任何修改的文件带有醒目的通知，说明您更改了文件;

      （c） 您必须以您分发的任何衍生作品的源形式保留作品源形式的所有版权、专利、商标和归属声明，
      	   但与衍生作品的任何部分无关的声明除外;

      （d） 如果作品包含“通知”文本文件作为其分发的一部分，则您分发的任何衍生作品必须至少在以下
      	   任一地方包含此类通知文件中包含的归属声明的可读副本，不包括与衍生作品任何部分无关的声明：
      	   在作为衍生作品的一部分分发的通知文本文件中;在源表单或文档中，如果与衍生作品一起提供;
      	   或者，在衍生作品生成的显示中，如果以及在哪里通常出现此类第三方通知。通知文件的内容仅供参考，
      	   不修改许可证。您可以在您分发的衍生作品中添加您自己的归属声明，与作品的声明文本一起或作为附录，
      	   前提是此类附加归属声明不能被解释为修改许可。

      您可以在您的修改中添加您自己的版权声明，并可以为使用、复制或分发您的修改或任何此类衍生作品提供
      附加或不同的许可条款和条件，前提是您对本作品的使用、复制和分发符合本许可中规定的条件。
```

<h2 align=center>
    商业使用
</h2>


```
      未经版权所有者许可，不得将未经修改的源代码和软件用于商业用途。
      修改后的软件及其源代码可在修改者和原作者同意的情况下用于商业用途。
      授权修改者可以自行决定其修改后的软件和源代码是否可以在商业上获得。
```

<h2 align=center>
    附加条款
</h2>


```
	  版权声明和本许可应包含在软件的所有副本或大部分内容中。
	  软件和源代码的所有副本和修改版本均应根据本许可进行分发和分发，除非版权所有者特别允许
```

**条款和条件结束**

​																																																  1.0版本，2022年11月

​																																																			GeorgeDong32
