using Formula1App.Models;
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
            if (container is CollectionView collectionView && collectionView.ItemsSource is IEnumerable items)
            {
                var itemList = items.Cast<object>().ToList(); // Convert to List<object>
                int index = itemList.IndexOf(item); // Get index of the current item

                return (index % 2 == 0) ? OddTemplate : EvenTemplate;
            }

            return OddTemplate; // Default fallback
        }
    }
    public class MyDataTempSelectorLog : DataTemplateSelector
    {
        public DataTemplate NotLoggedTemplate { get; set; }
        public DataTemplate LoggedTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (container is CollectionView collectionView && collectionView.ItemsSource is IEnumerable items)
            {
                var itemList = items.Cast<object>().ToList(); // Convert to List<object>

                return (((UserWType)item).Id == ((App)Application.Current).LoggedUser.Id) ? LoggedTemplate : NotLoggedTemplate;
            }

            return NotLoggedTemplate; // Default fallback
        }
    }
    public class MyDataTempSelectorIsBreaking : DataTemplateSelector
    {
        public DataTemplate BreakingTemplate { get; set; }
        public DataTemplate NotBreakingTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (container is CollectionView collectionView && collectionView.ItemsSource is IEnumerable items)
            {
                var itemList = items.Cast<object>().ToList(); // Convert to List<object>

                return (((Article)item).IsBreaking ? BreakingTemplate : NotBreakingTemplate);
            }

            return NotBreakingTemplate; // Default fallback
        }
    }
}
