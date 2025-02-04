
namespace UC.LADI.MAG.WEB.Services
{
	public interface IServiceWrapper
	{
        mag_collection.IService mag_collection { get; }
        mag_event.IService mag_event { get; }
        mag_image.IService mag_image { get; }
        mag_item.IService mag_item { get; }
        PublicImage.IService PublicImage { get; }

        PublicEvent.IService PublicEvent { get; }

        PublicItem.IService PublicItem { get; }
        PublicCollection.IService PublicCollection { get; }

    }
}
