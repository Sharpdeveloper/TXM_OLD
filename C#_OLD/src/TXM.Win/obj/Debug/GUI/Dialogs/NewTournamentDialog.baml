   M S B A M L   `   `   `  ����  TXM)    #TXM.GUI.Dialogs.NewTournamentDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Create new Tournament��5   	   $��376.244��6'   $��494.561��68    � 
ResizeMode$ NoResize=�6H   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��203* �6   �� 5      $
��286* �6   .+�5
   
   �� -   5         ��Name$ LabelTournamentForm��5
      $��Choose Tournamenttype:��6-   $
��Left=�6N   $��	12,43,0,0q�6i   $	��Top=�6|   $��33��6�   �� 5   
   -   5         ��Name$ ComboBoxTournamentType��5      $
��Left=�63   $��5,43,0,0q�6N   $	��Top=�6`   $	��263��6x   $��1��6�   �� 5   
   -   5        $ LabelCut��5      $
��Cut?��6"   $
��Left=�61   $��	12,76,0,0q�6L   $	��Top=�6_   $��33��6w   	� 5   
   -   5         	�Name$ RadioButtonTop4��5       	� 	GroupName$	 Cut��6/   $��Top 4��6?   $
��Left=�6O   $��
147,82,0,0q�6j   $	��Top=�6~   {�. $��1��6�   	� 5   
   -   5        $ RadioButtonTop8��5      $	 Cut��6/   $��Top 8��6?   $
��Left=�6O   $��	5,102,0,0q�6j   $	��Top=�6}   {�. $��1��6�   	� 5   
   -   5        $ RadioButtonTop16��5      $	 Cut��60   $��Top 16��6@   $
��Left=�6Q   $��147,102,0,0q�6l   $	��Top=�6�   {�. $��1��6�   	� 5   
   -   5        $ RadioButtonNoCut��5      $	 Cut��60   $��No Cut��6@   $
��Left=�6Q   $��5,82,0,0q�6l   $	��Top=�6~   {�.  d� 	IsChecked$
 TrueY�6�   $��1��6�   �� 5   
   -   5         ��Name$ TextBoxMaxSquad��5      $��	9,156,0,0q�6+   $
��Left=�6>   $	��259��6Y   $��19��6e   $	��Top=�6q   $��1��6�   �� 5   
   -	   5        	 ��Name$	 ButtonOK��5      $
��Save��6#   $
��Left=�62   $��108,313,0,0q�6M   $	��Top=�6b   $��72��6z   $��1��6�   �� 5   
   -
   5        $	 ButtonCancel��5      $��Cancel��6'   $
��Left=�68   $��194,313,0,0q�6S   $	��Top=�6h   $��75��6�   $��1��6�   �� 5   
   -   5        $ LabelTournamentName��5      $��Tournamentname:��6-   $
��Left=�6G   $��
12,122,0,0q�6b   $	��Top=�6v   �� 5   
   -   5        $ TextboxName��5      $
��Left=�6'   $��23��6B   $��	9,126,0,0q�6N   
 �� TextWrapping$

 Wrap=�6a   $	��Top=�6u   $	��259��6�   $��1��6�   �� 5   
   -   5        $ LabelListsize��5      $��Max Listpoints:��6'   $
��Left=�6A   $��
12,153,0,0q�6\   $	��Top=�6p   �� 5   
   -   5        $ label��5      $A��;First Round Teamprotection?
(only allowed in casual events)��6   $
��Left=�6i   $��
14,210,0,0q�6�   $	��Top=�6�   $	��187��6�   	� 5   
   -   5        $ radioButtonTPYes��5      $	��Yes��60   $ TeamProtection��6>   $
��Left=�6Y   $��
10,231,0,0q�6t   $	��Top=�6�   $��1��6�   	� 5   
   -   5        $ radioButtonTPNo��5      $
 trueY�6/   $ TeamProtection��6@   $��No��6[   $
��Left=�6h   $��148,231,0,0q�6�   $	��Top=�6�   $��1��6�   �� 5   
   -   5        $ label_Copy1��5      $��Tournamenttype:��6%   $
��Left=�6?   $��
14,257,0,0q�6Z   $	��Top=�6n   $	��194��6�   $��2��6�   	� 5   
   -   5        $ radioButtonTypeSingle��5      $
 TrueY�65   $��Single��6F   $
 Type��6W   $
��Left=�6h   $��
11,263,0,0q�6�   $	��Top=�6�   $��1��6�   	� 5   
   -   5        $ radioButtonTypeTeam��5      $ falseY�63   $
 Type��6E   $
��Team��6V   $
��Left=�6e   $��
79,263,0,0q�6�   $	��Top=�6�   $��1��6�   �� 5   
   -   5        $ LabelGameSystem��5      $��Choose Gamesystem:��6)   $
��Left=�6F   $��	13,10,0,0q�6a   $	��Top=�6t   $��33��6�   �� 5   
   -   5        $ ComboBoxGameSystem��5      $
��Left=�6/   $��5,10,0,0q�6J   $	��Top=�6\   $	��263��6t   $��1��6�   �� 5   
   -   5        $ label_Copy2��5      $3��-Only needed at games with Army or Squadlists.��6%   $
��Left=�6]   $��
14,175,0,0q�6x   $	��Top=�6�   $	��266��6�   $��2��6�   5!      5"      