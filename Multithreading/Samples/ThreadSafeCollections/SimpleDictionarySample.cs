using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Multithreading.Samples.ThreadSafeCollection
{
    internal class SimpleDictionarySample : ISample
    {
        private readonly ConcurrentDictionary<int, User> _app = new ConcurrentDictionary<int, User>();


        //private readonly Dictionary<int, User> _app = new Dictionary<int, User>();
        private readonly List<User> _users = new List<User>();

        public SimpleDictionarySample()
        {
            _users.Add(new User { Id = 1, FirstName = "John", LastName = "Doe" });
            _users.Add(new User { Id = 2, FirstName = "Johnny", LastName = "Public" });
            _users.Add(new User { Id = 3, FirstName = "Jan", LastName = "Kowalski" });
        }

        public void Run()
        {
            SimulateUsage();

            Console.ReadKey();
        }


        private void SimulateUsage()
        {
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    AddAndRemoveUsers();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{i} iteration. Exception: {ex.Message}");
                }
                finally
                {
                    _app.Clear();
                }
            }
        }

        private void AddAndRemoveUsers()
        {
            var addUsers = new List<Task>
            {
                Task.Run(() => LogIn(_users[0])),
                Task.Run(() => LogIn(_users[0])),
                Task.Run(() => LogIn(_users[1])),
                Task.Run(() => LogIn(_users[1])),
                Task.Run(() => LogIn(_users[1])),
                Task.Run(() => LogIn(_users[2]))
            };
            Task.WaitAll(addUsers.ToArray());

            DisplayLoggedInUsers();

            var removeUsers = new List<Task>
            {
                Task.Run(() => LogOut(_users[0])),
                Task.Run(() => LogOut(_users[0])),
                Task.Run(() => LogOut(_users[1])),
                Task.Run(() => LogOut(_users[1])),
                Task.Run(() => LogOut(_users[1])),
                Task.Run(() => LogOut(_users[2]))
            };
            Task.WaitAll(removeUsers.ToArray());

            DisplayLoggedInUsers();
        }

        private readonly object _objLock = new object();

        private void LogIn(User user)
        {
            if (!_app.ContainsKey(user.Id))
            {
                _app.AddOrUpdate(user.Id, user, (i, val) => { return _app[i]; });
            }
        }
        private void LogOut(User user)
        {
            if (_app.ContainsKey(user.Id))
            {
                _app.TryRemove(user.Id, out var u);
            }
        }

        private void DisplayLoggedInUsers()
        {
            if (_app.Count == 0)
            {
                Console.WriteLine("No users are logged in.");
                return;
            }
            else
            {
                Console.WriteLine("users logged in:");
            }

            foreach (var item in _app)
            {
                Console.WriteLine(item);
            }
        }
    }

    internal class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return string.Format("\tId={0}    FirstName={1}    LastName={2}", Id, FirstName, LastName);
        }
    }
}
