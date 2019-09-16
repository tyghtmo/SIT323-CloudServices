using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ConfigurationDataLibrary;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
[DataContract]
public class Service : IService
{
    [DataMember]
    public ConfigurationData configurationData;

	public string GetAllocations(ConfigurationData configData)
	{
        
        return string.Format(configData.LogFilePath);
	}

}
