   M S B A M L   `   `   `  ����l  hXceed.Wpf.Toolkit.LiveExplorer, Version=3.0.6251.17501, Culture=neutral, PublicKeyToken=3e4669d2f30244f4O,clr-namespace:Xceed.Wpf.Toolkit.LiveExplorerXceed.Wpf.Toolkit.LiveExplorer  5      �Hclr-namespace:Xceed.Wpf.Toolkit.LiveExplorer.Samples.DataGrid.Converters:Xceed.Wpf.Toolkit.LiveExplorer.Samples.DataGrid.Converters  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel 5      q=clr-namespace:Xceed.Wpf.Toolkit.LiveExplorer.Samples.DataGrid/Xceed.Wpf.Toolkit.LiveExplorer.Samples.DataGrid  5      H    BXceed.Wpf.Toolkit.LiveExplorer.Samples.DataGrid.Views.DataGridView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      >local,clr-namespace:Xceed.Wpf.Toolkit.LiveExplorer     5      q mDotNetProjects.Wpf.Extended.Toolkit, Version=1.0.6251.17496, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      Y UXceed.Wpf.DataGrid, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f45xcdg*http://schemas.xceed.com/wpf/xaml/datagrid  5      SconvHclr-namespace:Xceed.Wpf.Toolkit.LiveExplorer.Samples.DataGrid.Converters  5      H	compModel8clr-namespace:System.ComponentModel;assembly=WindowsBase  5      Ldatagrid=clr-namespace:Xceed.Wpf.Toolkit.LiveExplorer.Samples.DataGrid  5      -   5        -   'Xceed.Wpf.Toolkit.LiveExplorer.DemoView   Name$  _demo��5      "   VerticalScrollBarVisibility$ Disabled=�5         Title$ DataGrid��5         Description 5      J� 5   
   $��14(�6   $��Segoe)�6"   .*�65   .*�5      �� .�6   ��Toolkit for WPF includes a powerful and fast datagrid control with many improvements over the stock WPF datagrid. In addition to providing a modern experience with the smooth scrolling and fully animated Tableflow view (can be turned off), it also provides zero-lag background data virtualization. Try a basic sample here. There are too many features and options to showcase in this sample. An even more feature-rich edition of this datagrid is available6�   �� 5      -   5        $5��/http://xceed.com/Grid_WPF_Intro.html?adtype=154?�5      .�6�   here.5!      5"       � 	Resources 5%      2  ,Xceed.Wpf.DataGrid.Converters.ImageConverter   imageConverterR   LXceed.Wpf.Toolkit.LiveExplorer.Samples.DataGrid.Converters.FlagPathConverter  flagPathConverter  employeeDataTemplate � ColumnDefinitions �� 	Converter �� NotifyOnValidationError �� ValidatesOnDataErrors	 �� Orientation  freightDataTemplate
 �� StringFormat  countryDataTemplate��%�  &	        &	      &	 &     7   &	 �    &	 P    7    5&      6     5(      6   �� 5+      .R�5,      � $��30��6   $��Center=�5-       5.      �� 5/      $
��Auto �6"   �� 50      .+�52      �� $��
0, 0, 5, 0q�6   ��53      �� *Photo+8    .   .  �� 55      $��1��6   $	 
Horizontal=�56      $��Center=�57      .+�58      �� ��6   �� *	FirstName+ .   .  �� 59      $�� ��6   �� 5:      ��6   �� *
LastName+ .   .  5;      5<      5=   	   �� 5@      .R�5A      �� $��Center=�5B      ��5A      �� $
 {0:C}��5B      5C   	   �� 5F      .R�5G      �� $	 
Horizontal=�6   $��Center=�5H      $��5,0,0,0q�5I      .+�5L      �� $��15��6   $��
0, 0, 5, 0q�5M      ��5N      �� 8   �� 5P      $��WordEllipsis=�5Q      $��Center=�5R      ��5P      �� 5R      5S      5T   	   5V      .��5X      �  5Y   
   5  /Xceed.Wpf.DataGrid.DataGridCollectionViewSource  	cvsOrders9  3Xceed.Wpf.DataGrid.DataGridCollectionViewSourceBase   Source �� GroupDescriptions2  ,System.Windows.Data.PropertyGroupDescription   PropertyName��%�   &	         5Z       5[   0   ��  5\        5]      $ ShipCountry��6/     5^      $ ShipCity��6/   5`      5a      .+�5c   
   (  "Xceed.Wpf.DataGrid.DataGridControl  -   5          Name$ 	_dataGrid��5c         	AllowDrag . ��5d      ��  �� Source# [    View 5f      ,  &Xceed.Wpf.DataGrid.Views.TableflowView  5g      (	  "Xceed.Wpf.DataGrid.Views.TableView 	  FixedColumnCount$ 2��6%      Columns 5j      
  Xceed.Wpf.DataGrid.Column
  5k      #  Xceed.Wpf.DataGrid.ColumnBase   	FieldName$ OrderID��6      Title$ Order��5l         Width8  2Xceed.Wpf.DataGrid.Converters.ColumnWidthConverter$	 100 5m         IsMainColumn . 5o      
  5q      $ Employee��6   $ Employee��5r      $	 160 5s         CellContentTemplate# [ 5t         GroupValueTemplate# [ 5u      
  5w      $ Customer.CompanyName��6   $ Customer��5x      $	 185 5y      
  5{      $ 	OrderDate��6   $ 
Order Date��5|      $	 120 5}      
  5      $ Shipper.CompanyName��6   $ Ship Via��5�      
  5�      $ Freight��6   $	 100 5�      # [ 5�      # [ 5�      
  5�      $ ShipCity��6   $
 City��5�      $ 80 5�      
  5�      $ ShipCountry��6   $ Country��5�      # [ 5�      # [ 5�      
  5�      $ ShipAddress��6   $ Address��5�      $	 200 5�      
  5�      $ ShipPostalCode��6   $ Postal Code��5�      $	 120 5�      
  5�      $ RequiredDate��6   $ Required Date��5�      $	 140 5�      
  5�      $ ShippedDate��6   $ Shipped Date��5�      $	 140 5�      
  5�      $ ShipName��6      Visible .  5�      
  5�      $ 
ShipRegion��6    .  5�      
  5�      $ 
EmployeeID��6    .  5�      
  5�      $ 
CustomerID��6    .  5�      
  5�      $ Customer��6    .  5�      
  5�      $ ShipVia��6    .  5�      
  5�      $ Shipper��6    .  5�      5�      5�      5�      