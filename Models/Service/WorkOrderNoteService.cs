using Abstracts.Models.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Service
{
    public class WorkOrderNoteService : BaseService, IWorkOrderNoteService
    {
        public void Create(WorkOrderNotes workOrderNotes)
        {
            using (var db = GetContext())
            {
                var entity = new WorkOrderNotes();

                db.WorkOrderNotes.Add(entity);
                db.SaveChanges();
                workOrderNotes.WorkOrderNoteId = (int)entity.WorkOrderNoteId;
            }
        }

        public IEnumerable<Refactored.WorkOrderNotes> Read(long RequestID)
        {
            using (var db = GetContext())
            {
                var result = db.WorkOrderNotes.Where(x => x.RequestId == RequestID).ToList().OrderByDescending(x => x.WorkOrderNoteId)
                             .ToList();

                return result;
            }
        }

        public IEnumerable<WorkOrderNotes> ReadByID(long workOrderNoteID)
        {
            using (var db = GetContext())
            {
                var result = db.WorkOrderNotes.ToList().OrderByDescending(x => x.WorkOrderNoteId).Where(x => x.WorkOrderNoteId == workOrderNoteID)
                             .ToList();
                return result;
            }
        }

        public void Update(WorkOrderNotes workOrderNotes)
        {
            using (var db = GetContext())
            {

                db.WorkOrderNotes.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}