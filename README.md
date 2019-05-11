# learning-forARCarsProject
first week's work for ARcarProject.

This module can read and write Xml file in iOS and macOS
(This is actually not my main purpose, this module is only a small part of my big project named ARCars, but I think the implementation of this module is a bit difficult, maybe my module can help someone who are troubled by this. )

这个模块能够阅读\写入xml, 在iOS和macOS已经测试通过
(通过一种自己想到的强行初始化的方法, 性能还不错)
xml存在persistent目录下(iOS的document下),被我这个AR汽车的项目充当一个数据库缓存的作用.


原理:
persistentData无法在打包的时候带入元数据，但是可以在运行时刻进行读写。streamingAssets可以在打包的时候带入元数据，但是运行时不可读写。所以实现缓存功能的解决方案就是先用streamingAssets打包带入元数据，然后在程序最开始运行时进行初始化，将元数据拷贝进入persistentData，随后用进入persistentData的元数据副本进行后续操作即可实现缓存。


懒得翻译成英文了= =不是专业的程序设计人员, 有问题也不一定马上更新.
