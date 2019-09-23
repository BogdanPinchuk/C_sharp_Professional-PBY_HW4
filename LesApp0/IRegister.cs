namespace LesApp0
{
    interface IRegister
    {
        string Login { get; }
        string Password { get; }
        bool Successful { get; }

        void RegisterUser();
    }
}