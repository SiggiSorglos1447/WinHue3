   M S B A M L   `   `   `  ����l  hXceed.Wpf.Toolkit.LiveExplorer, Version=3.0.6251.17501, Culture=neutral, PublicKeyToken=3e4669d2f30244f4O,clr-namespace:Xceed.Wpf.Toolkit.LiveExplorerXceed.Wpf.Toolkit.LiveExplorer  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      �Fclr-namespace:Xceed.Wpf.Toolkit.LiveExplorer.Samples.Window.Converters8Xceed.Wpf.Toolkit.LiveExplorer.Samples.Window.Converters  5      I    CXceed.Wpf.Toolkit.LiveExplorer.Samples.Window.Views.ChildWindowView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      >local,clr-namespace:Xceed.Wpf.Toolkit.LiveExplorer     5      q mDotNetProjects.Wpf.Extended.Toolkit, Version=1.0.6251.17496, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      0sys&clr-namespace:System;assembly=mscorlib  5      QconvFclr-namespace:Xceed.Wpf.Toolkit.LiveExplorer.Samples.Window.Converters  5      -   'Xceed.Wpf.Toolkit.LiveExplorer.DemoView    Title$  ChildWindow��5         Description 5      J� 5   
   $��14(�6   $��Segoe)�6"   .*�65   .*�5      �� .�6   |zThe ChildWindow control can be used to create a window that is displayed within the bounds of a specific parent container.6�   �� 5      �� 5      �� 5      .�6   ��In addition to the following features, the "Plus" version allows ChildWindow to be resizable and to use a Maximize/Restore button.6�   �� 5      �� 5      -   5        $2��,https://xceed.com/xceed-toolkit-plus-for-wpf?�5      .�6   ?=Click here for more details about Xceed Toolkit Plus for WPF.5!      5"       � 	Resources 5%      Q  KXceed.Wpf.Toolkit.LiveExplorer.Samples.Window.Converters.IntToBoolConverter   IntToBoolConverterA  ;Xceed.Wpf.Toolkit.Core.Converters.VisibilityToBoolConverter  VisibilityToBoolConverter   Inverted  WindowStates S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� TypeName  WindowStartupLocations��%A  &	        &	      &	 -     &	 �       5&      6      5'       . 6I   S� 5)      $ 	GetValues��60   "  5*       5+      M� 5,      $ xctk:WindowState��6   5.   	   S� 5/      $ 	GetValues��6:   "  50       51      M� 52      $  xctk:WindowStartupLocation��6   54   	   55      .��58      �  � RowDefinitions 59      �� 5:      $
��Auto �6   �� 5;      $��* �6   .+�5>      �� $��Features��6   $��0��6#   $��5q�60   .��5?      � $��5q�6    5@      �� 5A      $
��Auto �6   �� 5B      $
��Auto �6   �� 5C      $
��Auto �6   	 � ColumnDefinitions	 5E      �� 5F      $
��Auto �6"   �� 5G      $
��Auto �6"   �� 5H      $
��Auto �6"   �� 5I      $
��Auto �6"   .+�5L      �� $��Caption:��6   $��2q�6(   $��Center=�63   �� 5M      -   5        $��_txtCaption��5M      $��1��6)   $��5q�69   $��ChildWindow��6D   $	��150��6W   $��Center=�6c   �� 5O      -   5        $��
textBlock2��5O   %   $��1��6   $��Left:��67   $��2q�6D   $��Center=�6O     DisableGrayText#��[ 6�   {�6j   �� *	IsEnabled+
 �� ElementName$
 _txtLeft��6�   $  Xceed.Wpf.Toolkit.DoubleUpDown  5P      -   5          Name$ _txtLeft��5P       $��1��62   $��1��6B   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$ 0X�6O      Minimum$ 0X�6Y   $��Center=�6e   $��5q�6�   {�6�   �� *SelectedIndex+$ 
 _cboWindowStartupLocations���� 5R      -   5        $��
textBlock3��5R   %   $��2��6   $
��Top:��67   $��2q�6C   $��Center=�6N   #��[ 6�   {�6i   �� *	IsEnabled+$
 _txtTop��6�     5S      -   5        $ _txtTop��5S       $��1��61   $��2��6A   $ 0X�6N   $ 0X�6X   $��Center=�6d   $��5q�6   {�6�   �� *SelectedIndex+$ 
 _cboWindowStartupLocations���� 5U      $��IsModal:��6   $��Center=�6(   $��10,2,2,2q�6C   $��2��6U   �� 5V      -   5         ��Name$ _chkIsModal��5V      $��3��6,   $��Center=�6<   $��5q�6W    d� 	IsChecked$
 TrueY�6b   �� 5X      $��1��6   $��WindowStartupLocation:��6%   $��Center=�6C   $��10,2,2,2q�6^   $��2��6p   #��[ 6�   {�6�   �� *	IsEnabled+$ 
 _cboWindowStartupLocations��6�   �� 5Y      -   5        $ ��_cboWindowStartupLocations��5Y   4   $��3��6   $��1��6'    �� SelectedIndex$ 1��6�   $��Center=�6�   $��70��6�   $��5q�6�   ��6V   ��  �� Source# [ {�6�   �� *SelectedIndex+$
 _cboWindowState�� �� 	Converter# [  �� 5[      $��2��6   $��WindowState:��6%   $��Center=�69   $��10,2,2,2q�6T   $��2��6f   �� 5\      -	   5        $��_cboWindowState��5\   4   $��3��6   $��2��6'   $ 1��6�   $��Center=�6�   $��5q�6�   ��6K   �� # [ 6�   5]      5^   	   � 5`      $��1��6   $��10q�6   .+�5a   
   2  ,Xceed.Wpf.Toolkit.Primitives.WindowContainer     ModalBackgroundBrush
 �����.+�5b      #  Xceed.Wpf.Toolkit.ChildWindow  -
   5          Name$ _childWindow��5b      $	��350��5c      $	��220��5d      0	  *Xceed.Wpf.Toolkit.Primitives.WindowControl 	  Caption 5e      �� *Text+$
 _txtCaption��   IsModal 5f      �� *	IsChecked+$
 _chkIsModal�� 	  Left 5g      �� *Value+$
 _txtLeft�� �� Mode$ TwoWay=�
 	  Top 5h      �� *Value+$
 _txtTop��$ TwoWay=�   WindowStartupLocation 5i      �� *SelectedItem+$ 
 _cboWindowStartupLocations��   WindowState 5j      �� *SelectedItem+$
 _cboWindowState��.��5k      � $��10q�6    5l      �� 5m      $
��Auto �6"   �� 5n      $
��Auto �6"   �� 5o      $
��Auto �6"   �� 5p      $
��Auto �6"   �� 5q      $
��Auto �6"   �� 5r      $
��Auto �6"   	 5t      �� 5u      $
��Auto �6%   �� 5v      $��* �6%   .+�5y      �� $��0��6   $��First Name:��6(   $��2q�6;   $��Center=�6F   (
  "Xceed.Wpf.Toolkit.WatermarkTextBox
  5z      $��0��6'   $��1��64   $��2q�6D   $��Center=�6O    
  	Watermark$ Enter First Name��6j   �� 5|      $��1��6   $��
Last Name:��6(   $��2q�6:   $��Center=�6E   
  5}      $��1��6'   $��1��64   $��2q�6D   $��Center=�6O   $ Enter Last Name��6j   �� 5      $��2��6   $��Date of Birth:��6(   $��2q�6>   $��Center=�6I   &   Xceed.Wpf.Toolkit.DateTimeUpDown  5�      $��2��6%   $��1��62      Format$ 	ShortDate=�6B   $��2q�6U   $��Center=�6`   ,  &Xceed.Wpf.Toolkit.Primitives.InputBase   	Watermark$ Enter Date of Birth��6{   �� 5�      $��3��6   $��Salary:��6(   $��2q�67   $��Center=�6B   %  Xceed.Wpf.Toolkit.IntegerUpDown  5�      $��3��6$   $��1��61   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$ C2��6A   $��2q�6S   $��Center=�6^   $ Enter Salary��6y   �� 5�      $��4��6   $��Favorite Color:��6(   $��2q�6?   $��Center=�6J   #  Xceed.Wpf.Toolkit.ColorPicker  5�      $��4��6"   $��1��6/   $��2q�6?   $��Center=�6J   �� 5�      $��5��6   $��2��6)     �� Orientation$  
Horizontal=�6=   $��Center=�6V   $��0,10,0,0q�6s   .+�5�      �� $��65��6   $��2q�6&   .��61   OK65   �� 5�      $��65��6   $��2q�6&   .��61   Cancel69   5�      5�      5�      5�      5�   	   5�      5�      