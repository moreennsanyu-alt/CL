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

// ServiceStack
global using ServiceStack;
global using ServiceStack.Text;

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
