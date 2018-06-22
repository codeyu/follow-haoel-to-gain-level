## 文章：[A Simple Explanation of OOP](https://www.codeproject.com/Articles/1248442/A-Simple-Explanation-of-OOP)

### 阅读报告
本篇文章作者试图对面向对象编程做一个简单的解释。        
首先，作者认为能正确的书写面向对象的程序并不容易。  
接着，作者引用了他在 Quora 网站上一个问题（[面向对象编程的独特优势是什么？](http://qr.ae/TUp2w2)）的答案。  
在答案中，作者认为面向对象编程最大的好处是`扩展性`，同时易于理解和维护。  
通过这个答案，作者想进一步用最简单的方式解释OOP。  
作者列举了三种结构的代码：  
第一种是充斥着各种条件语句的一泻千里式的代码。这种代码很难阅读和理解，更不用谈扩展性了。  
第二种代码结构把一些代码片段组织成函数，这种程序被称为`过程式编程`。但是，随着程序规模的扩大，这种程序也会失控。作者说，可以通过引入“模块”的概念改善这种状况。  
第三种是模块式的代码结构，作者认为，模块是函数和数据结构（函数会引用）的集合。一般来说，一个模块代表了某种内聚的抽象 - 数据结构独立为有意义的东西，并且和函数一起组成一个模块。  
文章写到这里，作者引入本文的重点：有没有比模块更好的组织代码的方式？也就是说，更高级别的代码抽象。  
在“类（class），对象（object）和消息（message）”这个小节，作者谈到对象比模块更好的优越性：  
* 对象比模块更具有内聚性，对象封装的数据对外部世界是隐藏的。
* 对象与对象之间应该通过发送消息来进行通信。
* 对象不是抽象数据类型（Abstract Data Type），作者引用[OOP vs FP](https://blog.cleancoder.com/uncle-bob/2014/11/24/FPvsOO.html)这篇文章的观点：对象是功能包，不是数据包。
* 对象可以继承另一对象（父对象）的数据和行为，并添加额外的数据和行为。
* 对象具有多态性。
* 对象通过`类（class）`创建，当我们从一个类创建一个对象时，我们说我们`实例化`了这个类。

最后，作者总结到：无论你怎么写代码，你要面对的问题是如何抽象数据和行为。所以，你要尽一切办法合理的封装数据和行为（功能）。

为了进一步了解面向对象编程（OOP），作者推荐了medium上的一篇文章：[什么是面向对象编程？](https://medium.com/learn-how-to-program/chapter-3-what-is-object-oriented-programming-d0a6ec0a7615)

读者按：纸上得来终觉浅，你只有经常写代码，写大量的代码，才能写出结构良好的程序。如果再有高手 review 你的代码，效果更加：）