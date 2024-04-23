using SharpBase.DI;
using SharpBase.Tasks;
using UnityEngine;

namespace UnityBase.GameCore
{
    public class GameStarter : MonoBehaviour
    {
        private void Start()
        {
            GlobalContainer.Instance.Init(InitOrder.Instance.GetTypes());

            InitOrder initOrder = InitOrder.Instance;
            
            TasksQueue tasksQueue = new();

            foreach (var type in initOrder.GetExecuteTypes())
            {
                var element = GlobalContainer.Instance.Get(type) as IExecutable;
                tasksQueue.AddTask(element.Execute());
            }
            
            tasksQueue.Start();
            
            //Обращаться к загрузочному экрану через менеджер загрузок
            //Задача на выкачку конфигов
            //Задача на авторизацию
            // tasksQueue.AddTask(new TaskSceneLoading("UI", true));
            // tasksQueue.AddTask(new TaskSceneLoading("Game", false));
        }
    }
}