﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using HangulClockKit;
using HangulClockDataKit;
using HangulClockDataKit.Model;

namespace HangulClock
{
    /// <summary>
    /// DashboardTab.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CommentSettingTab : UserControl
    {
        private CommentSettingsByMonitor monitorSetting;

        public CommentSettingTab()
        {
            InitializeComponent();
        }

        public void loadInitData()
        {
            monitorSetting = MainWindow.loadCommentPreferences();

            commentSettingONToggle.IsChecked = monitorSetting.IsEnabled;
            commentFromServerToggle.IsChecked = monitorSetting.IsEnabledLoadFromServer;

            commentField.IsEnabled = commentSettingONToggle.IsChecked ?? false;
            nameJongsungText.Opacity = commentSettingONToggle.IsChecked ?? false ? 1 : 0.3;
            commentNameField.IsEnabled = commentSettingONToggle.IsChecked ?? false;

            commentNameField.Text = monitorSetting.Name;
            commentField.Text = monitorSetting.Comment;

            saveCommentPosition(monitorSetting.DirectionOfComment);
        }

        private void commentSettingONToggle_Checked(object sender, RoutedEventArgs e)
        {
            DataKit.getInstance().getSharedRealms().Write(() =>
            {
                monitorSetting.IsEnabled = true;
            });

            commentField.IsEnabled = true;
            nameJongsungText.Opacity = 1;
            commentNameField.IsEnabled = true;
        }

        private void commentSettingONToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            DataKit.getInstance().getSharedRealms().Write(() =>
            {
                monitorSetting.IsEnabled = false;
            });

            commentField.IsEnabled = false;
            nameJongsungText.Opacity = 0.3;
            commentNameField.IsEnabled = false;
        }

        private void commentFromServerToggle_Checked(object sender, RoutedEventArgs e)
        {
            DataKit.getInstance().getSharedRealms().Write(() =>
            {
                monitorSetting.IsEnabledLoadFromServer = true;
            });
        }

        private void commentFromServerToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            DataKit.getInstance().getSharedRealms().Write(() =>
            {
                monitorSetting.IsEnabledLoadFromServer = false;
            });
        }

        private void commentNameField_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataKit.getInstance().getSharedRealms().Write(() =>
            {
                monitorSetting.Name = commentNameField.Text;
            });

            try
            {
                HangulKit.HANGUL_INFO partOfName = HangulKit.HangulJaso.DevideJaso(commentNameField.Text[commentNameField.Text.Length - 1]);
                if (partOfName.chars[2] == ' ')
                {
                    nameJongsungText.Content = "야, ";
                }
                else
                {
                    nameJongsungText.Content = "아, ";
                }
            }
            catch (NullReferenceException ee)
            {

            }
            catch (IndexOutOfRangeException ee)
            {

            }
        }

        private void commentField_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataKit.getInstance().getSharedRealms().Write(() =>
            {
                monitorSetting.Comment = commentField.Text;
            });
        }

        private void setButtonHoverEnterEvent(Grid container)
        {
            BrushConverter bc = new BrushConverter();
            container.Background = (Brush)bc.ConvertFrom("#99FFFFFF");
        }

        private void setButtonHoverOutEvent(Grid container)
        {
            BrushConverter bc = new BrushConverter();
            container.Background = (Brush)bc.ConvertFrom("#03FFFFFF");
        }

        private void saveCommentPosition(int direction)
        {
            DataKit.getInstance().getSharedRealms().Write(() =>
            {
                monitorSetting.DirectionOfComment = direction;
            });

            BrushConverter bc = new BrushConverter();

            commentPositionTopContainer.Background = (Brush)bc.ConvertFrom("#03FFFFFF");
            commentPositionLeftContainer.Background = (Brush)bc.ConvertFrom("#03FFFFFF");
            commentPositionRightContainer.Background = (Brush)bc.ConvertFrom("#03FFFFFF");
            commentPositionBottomContainer.Background = (Brush)bc.ConvertFrom("#03FFFFFF");

            if (direction == CommentSettingsByMonitor.CommentDirection.TOP)
            {
                commentPositionTopContainer.Background= (Brush)bc.ConvertFrom("#FFFFFF");
            }
            else if (direction == CommentSettingsByMonitor.CommentDirection.LEFT)
            {
                commentPositionLeftContainer.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            }
            else if (direction == CommentSettingsByMonitor.CommentDirection.RIGHT)
            {
                commentPositionRightContainer.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            }
            else
            {
                commentPositionBottomContainer.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            }

            Debug.WriteLine("OK");
        }

        #region Comment Up Container Event Handler
        private void commentPositionTopContainer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (monitorSetting.DirectionOfComment != CommentSettingsByMonitor.CommentDirection.TOP)
            {
                setButtonHoverEnterEvent(commentPositionTopContainer);
            }
        }

        private void commentPositionTopContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (monitorSetting.DirectionOfComment != CommentSettingsByMonitor.CommentDirection.TOP)
            {
                setButtonHoverOutEvent(commentPositionTopContainer);
            }
        }

        private void commentPositionTopContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            saveCommentPosition(CommentSettingsByMonitor.CommentDirection.TOP);
        }
        #endregion

        #region Comment Left Container Event Handler
        private void commentPositionLeftContainer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (monitorSetting.DirectionOfComment != CommentSettingsByMonitor.CommentDirection.LEFT)
            {
                setButtonHoverEnterEvent(commentPositionLeftContainer);
            }
        }

        private void commentPositionLeftContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (monitorSetting.DirectionOfComment != CommentSettingsByMonitor.CommentDirection.LEFT)
            {
                setButtonHoverOutEvent(commentPositionLeftContainer);
            }
        }

        private void commentPositionLeftContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            saveCommentPosition(CommentSettingsByMonitor.CommentDirection.LEFT);
        }
        #endregion

        #region Comment Right Container Event Handler
        private void commentPositionRightContainer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (monitorSetting.DirectionOfComment != CommentSettingsByMonitor.CommentDirection.RIGHT)
            {
                setButtonHoverEnterEvent(commentPositionRightContainer);
            }
        }

        private void commentPositionRightContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (monitorSetting.DirectionOfComment != CommentSettingsByMonitor.CommentDirection.RIGHT)
            {
                setButtonHoverOutEvent(commentPositionRightContainer);
            }
        }

        private void commentPositionRightContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            saveCommentPosition(CommentSettingsByMonitor.CommentDirection.RIGHT);
        }
        #endregion

        #region Comment Bottom Container Event Handler
        private void commentPositionBottomContainer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (monitorSetting.DirectionOfComment != CommentSettingsByMonitor.CommentDirection.BOTTOM)
            {
                setButtonHoverEnterEvent(commentPositionBottomContainer);
            }
        }

        private void commentPositionBottomContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (monitorSetting.DirectionOfComment != CommentSettingsByMonitor.CommentDirection.BOTTOM)
            {
                setButtonHoverOutEvent(commentPositionBottomContainer);
            }
        }

        private void commentPositionBottomContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            saveCommentPosition(CommentSettingsByMonitor.CommentDirection.BOTTOM);
        }
        #endregion
    }
}
