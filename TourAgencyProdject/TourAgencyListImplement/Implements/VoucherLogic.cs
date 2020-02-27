using System;
using System.Collections.Generic;
using System.Text;
using TourAgencyBusinessLogic.BindingModels;
using TourAgencyBusinessLogic.Interfaces;
using TourAgencyBusinessLogic.ViewModels;
using TourAgencyListImplement.Models;

namespace TourAgencyListImplement.Implements
{
    public class VoucherLogic : IVoucherLogic
    {
        private readonly DataListSingleton source;
        public VoucherLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(VoucherBindingModel model)
        {
            Voucher tempProduct = model.Id.HasValue ? null : new Voucher { Id = 1 };
            foreach (var product in source.Products)
            {
                if (product.ProductName == model.ProductName && product.Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (!model.Id.HasValue && product.Id >= tempProduct.Id)
                {
                    tempProduct.Id = product.Id + 1;
                }
                else if (model.Id.HasValue && product.Id == model.Id)
                {
                    tempProduct = product;
                }
            }
            if (model.Id.HasValue)
            {               
            if (tempProduct == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempProduct);
            }
            else
            {
                source.Products.Add(CreateModel(model, tempProduct));
            }
        }
        public void Delete(VoucherBindingModel model)
        {
            // удаляем записи по компонентам при удалении изделия
            for (int i = 0; i < source.Producttours.Count; ++i)
            {
                if (source.Producttours[i].ProductId == model.Id)
                {
                    source.Producttours.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id == model.Id)
                {
                    source.Products.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Voucher CreateModel(VoucherBindingModel model, Voucher product)
        {
            product.ProductName = model.ProductName;
            product.Price = model.Price;
            //обновляем существуюущие компоненты и ищем максимальный идентификатор
            int maxPCId = 0;
            for (int i = 0; i < source.Producttours.Count; ++i)
            {
                if (source.Producttours[i].Id > maxPCId)
                {
                    maxPCId = source.Producttours[i].Id;
                }
                if (source.Producttours[i].ProductId == product.Id)
                {
                    // если в модели пришла запись компонента с таким id
                    if
                    (model.Producttours.ContainsKey(source.Producttours[i].tourId))
                    {
                        // обновляем количество
                        source.Producttours[i].Count =
                        model.Producttours[source.Producttours[i].tourId].Item2;
                        // из модели убираем эту запись, чтобы остались только не просмотренные
                        model.Producttours.Remove(source.Producttours[i].tourId);
                    }
                    else
                    {
                        source.Producttours.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            foreach (var pc in model.Producttours)
            {
                source.Producttours.Add(new VoucherTour
                {
                    Id = ++maxPCId,
                    ProductId = product.Id,
                    tourId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return product;
        }
        public List<VoucherViewModel> Read(VoucherBindingModel model)
        {
            List<VoucherViewModel> result = new List<VoucherViewModel>();
            foreach (var tour in source.Products)
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
        private VoucherViewModel CreateViewModel(Voucher product)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
        Dictionary<int, (string, int)> producttours = new Dictionary<int,
        (string, int)>();
            foreach (var pc in source.Producttours)
            {
                if (pc.ProductId == product.Id)
                {
                    string tourName = string.Empty;
                    foreach (var tour in source.tours)
                    {
                        if (pc.tourId == tour.Id)
                        {
                            tourName = tour.tourName;
                            break;
                        }
                    }
                    producttours.Add(pc.tourId, (tourName, pc.Count));
                }
            }
            return new VoucherViewModel
            {
                Id = product.Id,               
            ProductName = product.ProductName,
                Price = product.Price,
                ProductComponents = producttours
            };
        }
    }
}
