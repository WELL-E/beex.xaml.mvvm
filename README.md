
##  ![](https://gitee.com/well.e/beex.xaml.mvvm/raw/master/beex.png)  beex.xaml.mvvm
Beex.Xaml.Mvvm是一个简单的WPF MVVM框架。主要是帮助解决Model与ViewModel、View之间的关系，ViewModel与ViewModel之间的关系，ViewModel与View之间的关系。没有使用`Naming conventions`，使用起来非常直观简单。

###包含的组件


- BindableBase:实现熟悉改变通知
- DelegateCommand、EventToCommand:事件转换为命令
- ServiceContainer:简单的Ioc容器
- ViewContainer:View容器，实现在ViewModel中非常优雅的获取View
- Messenger:消息传输者，解决ViewModel和ViewModel通讯问题
- PropertyValidationRule:属性验证正则表达式

###如何使用
Beex.VFirst.Sample是一个引用了Beex.Xaml.Mvvm的WPF程序框架，采用的是View First的方式实现的。关键代码如下：

	//要导航到的界面和界面提供界面初始化数据
	Messenger.Default.Send(new TransMessage(typeof(SecondView), User.ToModel()));

	...

	private void OnMessage(TransMessage model)
    {
		//通过View的类型，在容器中获取当前需要显示的View，并将View显示出来
        CurrentView =  IoC.Instance.Resolve(model.ViewType) as ContentControl;
		//通过View的DataContext属性获取ViewModel，并调用ViewModel中的初始化数据接口，实现界面初始化
        var vm = CurrentView.DataContext as IInitializeService;
        if (vm != null) vm.InitializeData(model);
    }

如有疑问请加QQ群大家一起学习讨论:146195995