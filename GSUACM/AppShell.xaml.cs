using GSUACM.Services;
using GSUACM.Views;
using GSUACM.Views.Chat;
using GSUACM.Views.Control_Panel;
using GSUACM.Views.Dashboard;
using GSUACM.Views.Polls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GSUACM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell, INotifyPropertyChanged
    {    
        public AppShell()
        {
            InitializeComponent();

            #region DASHBOARD REGION
            ShellSection DashboardSection = new ShellSection()
            {
                Title = "Dashboard"
            };
            DashboardSection.Items.Add(new ShellContent()
            {
                Content = new DashboardPage()
            });
            #endregion

            #region EVENTS REGION
            ShellSection EventsSection = new ShellSection()
            {
                Title = "Events"
            };
            EventsSection.Items.Add(new ShellContent()
            {
                Content = new EventsPage()
            });
            #endregion

            #region ABOUT REGION
            ShellSection AboutSection = new ShellSection()
            {
                Title = "About Us"
            };
            AboutSection.Items.Add(new ShellContent()
            {
                Content = new AboutPage()
            });
            #endregion

            MainShell.Items.Add(DashboardSection);

            if (GlobalVars.User == null)
            {
                MainShell.Items.Add(EventsSection);
                MainShell.Items.Add(AboutSection);
            }
            else if (GlobalVars.User != null)
            {

                #region PROFILE REGION
                ShellSection ProfileSection = new ShellSection()
                {
                    Title = "Profile"
                };
                ProfileSection.Items.Add(new ShellContent()
                {
                    Content = new HomePage()
                });
                #endregion

                #region MEMBERS REGION
                ShellSection MembersSection = new ShellSection()
                {
                    Title = "Members"
                };
                MembersSection.Items.Add(new ShellContent()
                {
                    Title = "Members",
                    Content = new MemberListPage()

                });
                MembersSection.Items.Add(new ShellContent()
                {
                    Title = "Mentors",
                    Content = new MentorListPage()
                });
                #endregion

                #region CHAT REGION
                ShellSection ChatSection = new ShellSection()
                {
                    Title = "Chat"
                };
                ChatSection.Items.Add(new ShellContent()
                {
                    Title = "Direct Messages",
                    Content = new MessageListPage()

                });
                ChatSection.Items.Add(new ShellContent()
                {
                    Title = "Public Channels",
                    Content = new ChannelListPage()
                });
                #endregion

                #region TUTORING REGION
                ShellSection TutoringSection = new ShellSection()
                {
                    Title = "Tutoring"
                };
                TutoringSection.Items.Add(new ShellContent()
                {
                    Content = new RequestTutor()
                });
                #endregion

                #region TUTORING REQUESTS REGION
                ShellSection TutoringRequestsSection = new ShellSection()
                {
                    Title = "Tutoring Requests"
                };
                TutoringRequestsSection.Items.Add(new ShellContent()
                {
                    Content = new TutoringRequestsPage()
                });
                #endregion

                #region POLLS REGION
                ShellSection PollsSection = new ShellSection()
                {
                    Title = "Polls"
                };
                PollsSection.Items.Add(new ShellContent()
                {
                    Title = "Active Polls",
                    Content = new ActivePollsPage()

                });
                PollsSection.Items.Add(new ShellContent()
                {
                    Title = "Past Polls",
                    Content = new PastPollsPage()
                });
                #endregion

                #region ALUMNI REGION
                ShellSection AlumniSection = new ShellSection()
                {
                    Title = "Alumni"
                };
                AlumniSection.Items.Add(new ShellContent()
                {
                    Content = new EmployerPage()
                });
                #endregion

                #region PAY DUES REGION
                ShellSection PayDuesSection = new ShellSection()
                {
                    Title = "Pay Dues"
                };
                PayDuesSection.Items.Add(new ShellContent()
                {
                    Content = new PayDuesPage()
                });
                #endregion

                if (GlobalVars.User.isAdmin)
                {

                    #region CONTROL PANEL REGION
                    ShellSection ControlPanelSection = new ShellSection()
                    {
                        Title = "Control Panel"
                    };
                    ControlPanelSection.Items.Add(new ShellContent()
                    {
                        Content = new ControlPanelPage()
                    });
                    #endregion

                    if (GlobalVars.User.isTutor)
                    {
                        MainShell.Items.Add(ControlPanelSection);
                        MainShell.Items.Add(EventsSection);
                        MainShell.Items.Add(ProfileSection);
                        MainShell.Items.Add(MembersSection);
                        MainShell.Items.Add(ChatSection);
                        MainShell.Items.Add(TutoringRequestsSection);
                        MainShell.Items.Add(TutoringSection);
                        MainShell.Items.Add(PollsSection);
                        MainShell.Items.Add(AlumniSection);
                        MainShell.Items.Add(PayDuesSection);
                        MainShell.Items.Add(AboutSection);
                    }
                    else
                    {
                        MainShell.Items.Add(ControlPanelSection);
                        MainShell.Items.Add(EventsSection);
                        MainShell.Items.Add(ProfileSection);
                        MainShell.Items.Add(MembersSection);
                        MainShell.Items.Add(ChatSection);
                        MainShell.Items.Add(TutoringSection);
                        MainShell.Items.Add(PollsSection);
                        MainShell.Items.Add(AlumniSection);
                        MainShell.Items.Add(PayDuesSection);
                        MainShell.Items.Add(AboutSection);
                    }
                }
                else if (GlobalVars.User.isTutor)
                {
                    MainShell.Items.Add(EventsSection);
                    MainShell.Items.Add(ProfileSection);
                    MainShell.Items.Add(MembersSection);
                    MainShell.Items.Add(ChatSection);
                    MainShell.Items.Add(TutoringRequestsSection);
                    MainShell.Items.Add(TutoringSection);
                    MainShell.Items.Add(PollsSection);
                    MainShell.Items.Add(AlumniSection);
                    MainShell.Items.Add(PayDuesSection);
                    MainShell.Items.Add(AboutSection);
                }
                else
                {
                    MainShell.Items.Add(EventsSection);
                    MainShell.Items.Add(ProfileSection);
                    MainShell.Items.Add(MembersSection);
                    MainShell.Items.Add(ChatSection);
                    MainShell.Items.Add(TutoringSection);
                    MainShell.Items.Add(PollsSection);
                    MainShell.Items.Add(AlumniSection);
                    MainShell.Items.Add(PayDuesSection);
                    MainShell.Items.Add(AboutSection);
                }
            }
        }
    }
}