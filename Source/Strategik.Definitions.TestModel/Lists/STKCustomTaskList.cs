using Strategik.Definitions.Fields;
using Strategik.Definitions.Lists;
using System;

namespace Strategik.Definitions.TestModel.Lists
{
    /// <summary>
    /// Defines a custom task list
    /// </summary>
    /// <remarks>
    /// Once defined, we can use our custom task list wherever we need it
    /// </remarks>
    public class STKCustomTaskList: STKTaskList
    {
        public STKCustomTaskList()
        {
            Define();
        }

        private void Define()
        {
            base.Name = "STKCustomTaskList";
            base.DisplayName = "My Custom Task List";
            base.Url = "STKTaskList";

            STKChoiceField exampleCustomField = new STKChoiceField()
            {
                Name = "STKProjectChoice",
                DisplayName = "Projects"
            };

            exampleCustomField.Choices.AddRange(new String[] { "Project 1", "Project 2", "Project 3" });
            base.Fields.Add(exampleCustomField);
        }
    }
}
