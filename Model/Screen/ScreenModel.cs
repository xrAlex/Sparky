using System;
using Common.DTO;
using Common.Entities;
using Common.Extensions.CollectionChanged;
using Common.Interfaces;

namespace Model.Screen
{
    public sealed partial class ScreenModel : IScreenModel
    {
        private readonly ScreenCollection.ScreenCollection _screenCollection = new();

        public event EventHandler<ScreensCollectionChangedArgs>? ScreensCollectionChanged;

        public ScreenModel()
        {
            ModelTest();
            _screenCollection.CollectionChanged += CollectionChanged;
        }

        // TODO: Test, remove later
        private void ModelTest()
        {
            _screenCollection.Add(new ScreenContextDTO(123, "TestScreeen1", "TestScreeen1"));
            _screenCollection.Add(new ScreenContextDTO(12345, "TestScreeen2", "TestScreeen2")
            {
                NightColorConfiguration = new ColorConfiguration(123, 123)
            });
            _screenCollection.Add(new ScreenContextDTO(123456, "TestScreeen3", "TestScreeen3")
            {
                NightColorConfiguration = new ColorConfiguration(123, 123)
            });
        }
    }
}
