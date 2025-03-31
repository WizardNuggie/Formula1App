using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ModelsExt
{
    public class MyDataTempSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate OddTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //var collectionView = container as CollectionView;
            //if (collectionView == null)
            //    return EvenTemplate;

            //var itemsSource = collectionView.ItemsSource as List<object>;
            //if (itemsSource == null)
            //    return EvenTemplate;

            //int index = itemsSource.IndexOf(item);
            //return (index % 2 == 0) ? EvenTemplate : OddTemplate;
            if (container is CollectionView collectionView && collectionView.ItemsSource is IEnumerable items)
            {
                var itemList = items.Cast<object>().ToList(); // Convert to List<object>
                int index = itemList.IndexOf(item); // Get index of the current item

                return (index % 2 == 0) ? OddTemplate : EvenTemplate;
            }

            return OddTemplate; // Default fallback
        }
    }
}
