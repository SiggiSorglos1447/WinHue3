   M S B A M L   `   `   `  ����l  hXceed.Wpf.Toolkit.LiveExplorer, Version=3.0.6251.17501, Culture=neutral, PublicKeyToken=3e4669d2f30244f4O,clr-namespace:Xceed.Wpf.Toolkit.LiveExplorerXceed.Wpf.Toolkit.LiveExplorer  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      L    FXceed.Wpf.Toolkit.LiveExplorer.Samples.AvalonDock.Views.AvalonDockView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      >local,clr-namespace:Xceed.Wpf.Toolkit.LiveExplorer     5      q mDotNetProjects.Wpf.Extended.Toolkit, Version=1.0.6251.17496, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      q mDotNetProjects.Wpf.AvalonDock.Themes.Metro, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4r nDotNetProjects.Wpf.AvalonDock.Themes.VS2010, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4p	 lDotNetProjects.Wpf.AvalonDock.Themes.Aero, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4k
 gDotNetProjects.Wpf.AvalonDock, Version=1.0.6251.17496, Culture=neutral, PublicKeyToken=3e4669d2f30244f4=xcad,http://schemas.xceed.com/wpf/xaml/avalondock   	 
 5      4s&clr-namespace:System;assembly=mscorlib   	 
 5      -   'Xceed.Wpf.Toolkit.LiveExplorer.DemoView    Title$  
AvalonDock��5         Description 5      J� 5      $��14(�6   $��Segoe)�6    .*�63   ��The AvalonDock layout system allows developers to create customizable layouts using a window docking system similar to what is found in many popular integrated development environements.5   	   .��5      �  � RowDefinitions 5      �� 5       $
��Auto �6   �� 5!      $
��Auto �6   �� 5"      $��* �6   .+�5%      �� $��1��6   $��Usage:��6    
  Header#��[  6,   ) 
 #Xceed.Wpf.AvalonDock.DockingManager  5'      $��2��6   $	��395��6)      AllowMixedOrientation . 
���   �$��1q�5*         DocumentHeaderTemplate 5+      �� 5,      .R�5-      ��  �� Orientation$ 
Horizontal=�6   .+�5.      �� $��0,0,4,0q�68   ��6   �� *
IconSource+68   �� 5/      ��6   �� *Title+50      51         Layout. 53      , 
 &Xceed.Wpf.AvalonDock.Layout.LayoutRoot  -   5        _layoutRoot��   	RootPanel. 54      - 
 'Xceed.Wpf.AvalonDock.Layout.LayoutPanel     Orientation$ 
Horizontal=�6   � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutPanelElement, DotNetProjects.Wpf.AvalonDock, Version=1.0.6251.17496, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]	   Children.	 55      6 
 0Xceed.Wpf.AvalonDock.Layout.LayoutAnchorablePane  � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutPositionableGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable, DotNetProjects.Wpf.AvalonDock, Version=1.0.6251.17496, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]
   	DockWidth$	
 200 �6+   � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable, DotNetProjects.Wpf.AvalonDock, Version=1.0.6251.17496, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children. 56      2	 
 ,Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable	  /
 
 )Xceed.Wpf.AvalonDock.Layout.LayoutContent 
  	ContentId$ 
properties��6*    
  Title$ 
Properties��6A    	  CanHide .   
  CanClose .   	  AutoHideWidth$	 240Q�57   -    
  
IconSource$! ../Images/property-blue.png��58   *    
  Content. 59      1  +Xceed.Wpf.Toolkit.PropertyGrid.PropertyGrid     NameColumnWidth$	 110Q�6)      AutoGenerateProperties .     SelectedObject 6?   ��  �� ElementName$ _layoutRoot�� �� Path$! LastFocusedDocument.Content�   PropertyDefinitions 5;      7  1Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinition  ;  5Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinitionBase   TargetProperties=  7Xceed.Wpf.Toolkit.PropertyGrid.Converters.ListConverter$ 
Background 65     5<      $ BorderBrush 65     5=      $ BorderThickness 65     5>      $ FontSize 65     5?      $ 	FontStyle 65     5@      $ Width 65     5A      $ Height 65   5C      5D      5E      9 
 3Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPaneGroup  5F      � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutDocumentPane, DotNetProjects.Wpf.AvalonDock, Version=1.0.6251.17496, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children. 5G      4 
 .Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPane  � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutContent, DotNetProjects.Wpf.AvalonDock, Version=1.0.6251.17496, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children. 5H      0 
 *Xceed.Wpf.AvalonDock.Layout.LayoutDocument  $ 	document1��6+   $ 
Document 1��6A   $ ../Images/document.png��6T   . 5I      �� $��Document 1 Content��6$   $��Center=�6A   $��Center=�6^   5J        5K      $ 	document2��6+   $ 
Document 2��6A   $ ../Images/document.png��6T   . 5L      �� $��Document 2 Content��6"    �� AcceptsReturn . 6<   5M      5N      5O      ; 
 5Xceed.Wpf.AvalonDock.Layout.LayoutAnchorablePaneGroup  5P      � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutPositionableGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutAnchorablePane, DotNetProjects.Wpf.AvalonDock, Version=1.0.6251.17496, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   	DockWidth$	 125 �60   � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutAnchorablePane, DotNetProjects.Wpf.AvalonDock, Version=1.0.6251.17496, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children. 5Q        . 5R      	  $ alarms��6-   $ Alarms��6@   $$ ../Images/alarm-clock-blue.png��6O   . 5S      �� .@�5T      ��Alarm 1��6/   ��5U      Alarm 2��6/   ��5V      Alarm 3��6/   5W      5X      	  5Y      $ journal��6-   $ Journal��6A   . 5Z      �� . �5[      /� .T�5\       J� $��14(�6*   $��Segoe)�68   .*�6K   .*�5^   #   *(This is the content of the Journal Pane.�� A �� 5`   #   .�6(   RichTextBox65    has been added here5a   !   5b      5c      5d      5e      5f      5g         LeftSide 5i      2 
 ,Xceed.Wpf.AvalonDock.Layout.LayoutAnchorSide  5j      � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutAnchorGroup, DotNetProjects.Wpf.AvalonDock, Version=1.0.6251.17496, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]    Children.  5k      3 
 -Xceed.Wpf.AvalonDock.Layout.LayoutAnchorGroup  . 5l      	  $ Agenda��6-   $ agenda��6<   $% ../Images/address-book-open.png��6O   . 5m      �� $��Agenda Content��6$   $��10q�6:   $��18(�6F   $��Black"�6T   $
��Wrap=�6g   5n      	  5o      $ Contacts��6-   $ contacts��6>   $( "../Images/address-book--pencil.png��6S   . 5p      �� $��Contacts Content��6$   $��10q�6<   $��18(�6H   $��Black"�6V   $
��Wrap=�6i   5q      5r      5s      5u      5v   	   5w      5x      