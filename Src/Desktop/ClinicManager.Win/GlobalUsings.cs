// Microsoft.Extensions: caching, configuration
global using Microsoft.Extensions.Caching.Memory;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Configuration.CommandLine;
global using Microsoft.Extensions.Configuration.EnvironmentVariables;
global using Microsoft.Extensions.Configuration.Json;

// Microsoft.Extensions: DI, hosting, http, resilience
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Http;
global using Microsoft.Extensions.Http.Resilience;
global using Microsoft.Extensions.Resilience;

// Microsoft.Extensions: logging, options, primitives
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Logging.Console;
global using Microsoft.Extensions.Logging.Debug;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Primitives;

// System
global using System.Net.Http;
global using System.Text.Json;
global using System.Text.RegularExpressions;
global using System.IO;

// Prism (v9 namespaces)
global using Prism.Commands;
global using Prism.DryIoc;
global using Prism.Events;
global using Prism.Ioc;
global using Prism.Modularity;
global using Prism.Mvvm;
global using Prism.Regions;

// CommunityToolkit.Mvvm
global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.Messaging;

// DryIoc / expression compiler
global using DryIoc;
global using DryIoc.Microsoft.DependencyInjection;
global using FastExpressionCompiler;

// WPF helpers
global using GongSolutions.Wpf.DragDrop;
global using Microsoft.Xaml.Behaviors;

// Async helpers
global using AsyncAwaitBestPractices;
global using AsyncAwaitBestPractices.MVVM;
global using AsyncKeyedLock;
global using EnumerableAsyncProcessor.Extensions;

// Resilience, guards, text
global using Polly;
global using Ardalis.GuardClauses;
global using Humanizer;

// Serilog: namespace is NOT imported globally because Serilog.ILogger
// clashes with Microsoft.Extensions.Logging.ILogger. Use aliases instead.
global using SerilogLog = Serilog.Log;
global using SerilogLogger = Serilog.ILogger;

global using ClinicManager.Win.Configuration;
global using ClinicManager.Win.Services;
global using ViewModelBase = ClinicManager.Win.Presentation.Mvvm.ViewModelBase;
global using ClinicManager.Win.Presentation.Mvvm;

global using Button = System.Windows.Controls.Button;
global using CheckBox = System.Windows.Controls.CheckBox;
global using ComboBox = System.Windows.Controls.ComboBox;
global using ContextMenu = System.Windows.Controls.ContextMenu;
global using Control = System.Windows.Controls.Control;
global using DataGrid = System.Windows.Controls.DataGrid;
global using GroupBox = System.Windows.Controls.GroupBox;
global using Image = System.Windows.Controls.Image;
global using Label = System.Windows.Controls.Label;
global using ListBox = System.Windows.Controls.ListBox;
global using ListView = System.Windows.Controls.ListView;
global using ListViewItem = System.Windows.Controls.ListViewItem;
global using MenuItem = System.Windows.Controls.MenuItem;
global using Panel = System.Windows.Controls.Panel;
global using PrintDialog = System.Windows.Controls.PrintDialog;
global using ProgressBar = System.Windows.Controls.ProgressBar;
global using RadioButton = System.Windows.Controls.RadioButton;
global using RichTextBox = System.Windows.Controls.RichTextBox;
global using ScrollBar = System.Windows.Controls.Primitives.ScrollBar;
global using StatusBar = System.Windows.Controls.Primitives.StatusBar;
global using TabControl = System.Windows.Controls.TabControl;
global using TextBox = System.Windows.Controls.TextBox;
global using ToolBar = System.Windows.Controls.ToolBar;
global using ToolTip = System.Windows.Controls.ToolTip;
global using TreeView = System.Windows.Controls.TreeView;
global using UserControl = System.Windows.Controls.UserControl;
global using WebBrowser = System.Windows.Controls.WebBrowser;

global using Application = System.Windows.Application;
global using Clipboard = System.Windows.Clipboard;
global using MessageBox = System.Windows.MessageBox;
global using DataFormats = System.Windows.DataFormats;
global using DragEventArgs = System.Windows.DragEventArgs;
global using Cursors = System.Windows.Input.Cursors;
global using KeyEventArgs = System.Windows.Input.KeyEventArgs;
global using MouseEventArgs = System.Windows.Input.MouseEventArgs;
global using Brushes = System.Windows.Media.Brushes;
global using Color = System.Windows.Media.Color;
global using FontFamily = System.Windows.Media.FontFamily;
global using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
global using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
