using System;
using System.Collections.Generic;
using System.Text;
using TourAgencyBusinessLogic.BindingModels;
using TourAgencyBusinessLogic.Interfaces;
using TourAgencyBusinessLogic.ViewModels;
using TourAgencyListImplement.Models;

namespace TourAgencyListImplement.Implements
{
    public class TourLogic : ITourLogic
    {
        private readonly DataListSingleton source;
        public TourLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(TourBindingModel model)
        {
            Tour temptour = model.Id.HasValue ? null : new Tour
            {
                Id = 1
            };
            foreach (var tour in source.tours)
            {
                if (tour.tourName == model.tourName && tour.Id !=
               model.Id)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
                if (!model.Id.HasValue && tour.Id >= temptour.Id)
                {
                    temptour.Id = tour.Id + 1;
                }
                else if (model.Id.HasValue && tour.Id == model.Id)
                    
                {
                    temptour = tour;
                }
            }
            if (model.Id.HasValue)
            {
                if (temptour == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, temptour);
            }
            else
            {
                source.tours.Add(CreateModel(model, temptour));
            }
        }
        public void Delete(TourBindingModel model)
        {
            for (int i = 0; i < source.tours.Count; ++i)
            {
                if (source.tours[i].Id == model.Id.Value)
                {
                    source.tours.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<TourViewModel> Read(TourBindingModel model)
        {
            List<TourViewModel> result = new List<TourViewModel>();
            foreach (var tour in source.tours)
            {
                if (model != null)
                {
                    if (tour.Id == model.Id)
                    {
                        result.Add(CreateViewModel(tour));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(tour));
            }
            return result;
        }
        private Tour CreateModel(TourBindingModel model, Tour tour)
        {
            tour.tourName = model.tourName;
            return tour;
        }
        private TourViewModel CreateViewModel(Tour tour)
        {
            return new TourViewModel
            {
            Id = tour.Id,
            tourName = tour.tourName
            };
        }
    }
}
