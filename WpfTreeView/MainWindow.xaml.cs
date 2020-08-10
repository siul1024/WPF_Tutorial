﻿using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfTreeView
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        /* summary */
        // Default constructor

        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion


        #region On Loaded
        /* summary */
        // When the application first opens
        // <param name="sender"></param>
        // <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                // Create a new item for it
                var item = new TreeViewItem()
                {
                    // Set the header
                    Header = drive,
                    // Add the full path
                    Tag = drive
                };
                
                
                // Add a dummy item
                item.Items.Add(null);

                // Listen out for item being expanded
                item.Expanded += Folder_Expanded;

                // Add it to the main TreeView
                FolderView.Items.Add(item);
            }
        }
        #endregion

        #region Folder Expanded
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks
            var item = (TreeViewItem)sender;

            // If the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
            {
                return;
            }

            // Clear dummy data
            item.Items.Clear();

            // Get full path
            var fullPath = (string)item.Tag;

            #endregion

            #region Get Folders

            // Create a blank list for directories
            var directories = new List<string>();

            // Try and get directories from the folder
            // ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                
                if(dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }

            }
            catch
            {
                /* path */
            }

            directories.ForEach(directoryPath =>
            {
                // Create directory item
                var subItem = new TreeViewItem()
                {
                    // Set header as folder name
                    //Header = System.IO.Path.GetDirectoryName(directoryPath),
                    Header = GetFileFolderName(directoryPath),

                    // And tag as full path
                    Tag = directoryPath
                };

                // Add dummy item so we can expand folder
                subItem.Items.Add(null);

                // Handle expanding
                subItem.Expanded += Folder_Expanded;

                // Add this item to the parent
                item.Items.Add(subItem);


            });
            #endregion

            #region Get Files

            // Create a blank list for files
            var files = new List<string>();

            // Try and get files from the folder
            // ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                {
                    files.AddRange(fs);
                }

            }
            catch
            {
                /* pass */
            }

            files.ForEach(filePath =>
            {
                // Create fileitem
                var subItem = new TreeViewItem()
                {
                    // Set header as file name
                    Header = GetFileFolderName(filePath),

                    // And tag as full path
                    Tag = filePath
                };

                // Add this item to the parent
                item.Items.Add(subItem);


            });
            #endregion
        }

        #endregion


        #region Helpers

        /* summary */
        // Find the file or folder name from a full path
        // <param name="path">the full path</param>
        // <returns></returns>


        public static string GetFileFolderName(string path)
        {
            // C:\Something\a folder
            // C:\Something\a file.png

            // If we have no path, return empty
            if(string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            // Make all slashes back slashes
            
            var normalizedPath = path.Replace('/', '\\');
            Console.WriteLine("normalizedPath: " + normalizedPath);

            // Find the last backslash in the path
            var lastIndedx = normalizedPath.LastIndexOf('\\');
            Console.WriteLine("lastIndedx: " + lastIndedx);

            // If we don't find a backslash, return the path itself
            if (lastIndedx <= 0)
            {
                return path;
            }

            // Return the name after the last back slash
            return path.Substring(lastIndedx + 1);
        }
        #endregion


    }
}
