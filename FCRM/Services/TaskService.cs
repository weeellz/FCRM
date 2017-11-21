using FSRM.Models;
using FSRM.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSRM.Services
{
    /// <summary>
    /// Спецификация - постановк задачи, которую менеджеры дают каждому исполнителю,
    /// Она состоит из небольших подзадач(Task), выполнение каждого из которых стоит определенную сумму денег
    /// Менеджер может добавлять задания в спецификацию или изменять их
    /// Исполнитель может просматривать свои задания и соглашаться с ценой
    /// </summary>
    public class TaskService
    {
        private SiteContext db = new SiteContext();
        private User authUser; //текущий пользователь
        public Result AddTask(Guid specificationId, Task task)
        {
            var specification = db.Tasks.Find(specificationId);
            if (specification.Order.Manager != authUser)
            {
                return new Result("Вы не являетесь менеджером этого проекта");
            }
            if (specification.Tasks.Contains(task))
            {
                return new Result("Это задание уже добавлено");
            }
            specification.Tasks.Add(task);
            db.SaveChanges();
            return Result.Empty;
        }

        public SpecificationResult GetSpectification(Guid orderId)
        {
            var specifications = db.Tasks.Where(task => task.Order.Guid == orderId && task.Preformer == authUser);
            if (specifications == null)
            {
                return new SpecificationResult("У вас нет заданий в этом проекте");
            }
            return new SpecificationResult(specifications.First());
        }
    }
}