emotes = {
	["/cop"] = { cmd = '/cop', event = 'playCopEmote' },
	["/sit"] = { cmd = '/sit', event = 'playSitEmote' },
	["/chair"] = { cmd = '/chair', event = 'playChairEmote' },
	["/kneel"] = { cmd = '/kneel', event = 'playKneelEmote' },
	["/medic"] = { cmd = '/medic', event = 'playMedicEmote' },
	["/notepad"] = { cmd = '/notepad', event = 'playNotepadEmote' },
	["/traffic"] = { cmd = '/traffic', event = 'playTrafficEmote' },
	["/photo"] = { cmd = '/photo', event = 'playPhotoEmote' },
	["/clipboard"] = { cmd = '/clipboard', event = 'playClipboardEmote' },
	["/lean"] = { cmd = '/lean', event = 'playLeanEmote' },
	["/smoke"] = { cmd = '/smoke', event = 'playSmokeEmote' },
	["/drink"] = { cmd = '/drink', event = 'playDrinkEmote' },
	["/cancelemote"] = { cmd = '/cancelemote', event = 'playCancelEmote' }
}

--RegisterNetEvent('printInvalidEmote');
RegisterNetEvent('printEmoteList');
RegisterNetEvent('playCopEmote');
RegisterNetEvent('playSitEmote');
RegisterNetEvent('playChairEmote');
RegisterNetEvent('playKneelEmote');
RegisterNetEvent('playMedicEmote');
RegisterNetEvent('playNotepadEmote');
RegisterNetEvent('playTrafficEmote');
RegisterNetEvent('playPhotoEmote');
RegisterNetEvent('playClipboardEmote');
RegisterNetEvent('playLeanEmote');
RegisterNetEvent('playSmokeEmote');
RegisterNetEvent('playDrinkEmote');
RegisterNetEvent('playCancelEmote');

playing_emote = false;

--[[
playing_cop_emote = false;
playing_sit_emote = false;
playing_chair_emote = false;
playing_kneel_emote = false;
playing_medic_emote = false;
playing_notepad_emote = false;
playing_traffic_emote = false;
playing_photo_emote = false;
playing_clipboard_emote = false;
playing_lean_emote = false;
playing_smoke_emote = false;
playing_drink_emote = false;
]]--

AddEventHandler('printEmoteList', function()
	TriggerEvent('chatMessage', "^4ALERT", {255, 0, 0}, "^2Emote List: ^0cop, sit, chair, kneel, medic, notepad, traffic, photo, clipboard, lean, smoke, drink");
end)

--AddEventHandler('printInvalidEmote', function()
--	TriggerEvent('chatMessage', "^4ALERT", {255, 0, 0}, "^1Invalid emote specified, use /emotes");
--end)

--!!!DO NOT EDIT BELOW THIS LINE!!!

AddEventHandler('playSmokeEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		TaskStartScenarioInPlace(ped, "WORLD_HUMAN_SMOKING", 0, true);
		playing_emote = true;
	end
	
	Menu.hidden = true
end)

AddEventHandler('playDrinkEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		TaskStartScenarioInPlace(ped, "WORLD_HUMAN_DRINKING", 0, true);
		playing_emote = true;
	end
	
	Menu.hidden = true
end)

AddEventHandler('playCopEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		TaskStartScenarioInPlace(ped, "WORLD_HUMAN_COP_IDLES", 0, true);
		playing_emote = true;
	end
	
	Menu.hidden = true
end)

AddEventHandler('playSitEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		TaskStartScenarioInPlace(ped, "WORLD_HUMAN_PICNIC", 0, true);
		playing_emote = true;
	end
	
	Menu.hidden = true
end)

AddEventHandler('playChairEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		pos = GetEntityCoords(ped);
		head = GetEntityHeading(ped);
		TaskStartScenarioAtPosition(ped, "PROP_HUMAN_SEAT_CHAIR", pos['x'], pos['y'], pos['z'] - 1, head, 0, 0, 1);
		--TaskStartScenarioInPlace(ped, "PROP_HUMAN_SEAT_CHAIR", 0, false);
		playing_emote = true;
	end
	
	Menu.hidden = true
end)

AddEventHandler('playKneelEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		TaskStartScenarioInPlace(ped, "CODE_HUMAN_MEDIC_KNEEL", 0, true);
		playing_emote = true;
	end
	
	Menu.hidden = true
end)

AddEventHandler('playMedicEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		TaskStartScenarioInPlace(ped, "CODE_HUMAN_MEDIC_TEND_TO_DEAD", 0, true);
		playing_emote = true;
	end
	
	Menu.hidden = true
end)

AddEventHandler('playNotepadEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		TaskStartScenarioInPlace(ped, "CODE_HUMAN_MEDIC_TIME_OF_DEATH", 0, true);
		playing_emote = true;
	end
	
	Menu.hidden = true
end)

AddEventHandler('playTrafficEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		TaskStartScenarioInPlace(ped, "WORLD_HUMAN_CAR_PARK_ATTENDANT", 0, false);
		playing_emote = true;
	end
	
	Menu.hidden = true
end)

AddEventHandler('playPhotoEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		TaskStartScenarioInPlace(ped, "WORLD_HUMAN_PAPARAZZI", 0, false);
		playing_emote = true;
	end
	
	Menu.hidden = true
end)

AddEventHandler('playClipboardEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		TaskStartScenarioInPlace(ped, "WORLD_HUMAN_CLIPBOARD", 0, false);
		playing_emote = true;
	end
	
	Menu.hidden = true
end)

AddEventHandler('playLeanEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		TaskStartScenarioInPlace(ped, "WORLD_HUMAN_LEANING", 0, true);
		playing_emote = true;
	end
	
	Menu.hidden = true
end)

AddEventHandler('playCancelEmote', function()
	ped = GetPlayerPed(-1);
	
	if ped then
		ClearPedTasks(ped);
		playing_emote = false
	end
	
	Menu.hidden = true
end)

function InitMenu()
	ClearMenu()
	Menu.addTitle("Emotes");
	Menu.addButton("Cop", "TriggerEvent", emotes["/cop"].event)
	Menu.addButton("Sit", "TriggerEvent", emotes["/sit"].event)
	Menu.addButton("Chair", "TriggerEvent", emotes["/chair"].event)
	Menu.addButton("Kneel", "TriggerEvent", emotes["/kneel"].event)
	Menu.addButton("Medic", "TriggerEvent", emotes["/medic"].event)
	Menu.addButton("Notepad", "TriggerEvent", emotes["/notepad"].event)
	Menu.addButton("Traffic", "TriggerEvent", emotes["/traffic"].event)
	Menu.addButton("Photo", "TriggerEvent", emotes["/photo"].event)
	Menu.addButton("Clipboard", "TriggerEvent", emotes["/clipboard"].event)
	Menu.addButton("Lean", "TriggerEvent", emotes["/lean"].event)
	Menu.addButton("Smoke", "TriggerEvent", emotes["/smoke"].event)
	Menu.addButton("Drink", "TriggerEvent", emotes["/drink"].event)
	Menu.addButton("Cancel emote", "TriggerEvent", emotes["/cancelemote"].event)
end

Citizen.CreateThread(function()
	while true do
		Citizen.Wait(0)
		if IsControlJustPressed(1, 173) then -- INPUT_CELLPHONE_DOWN
			InitMenu()                       
			Menu.hidden = not Menu.hidden    
		elseif IsControlJustPressed(1, 32) then -- INPUT_MOVE_UP_ONLY
			ClearPedTasks(ped);
			playing_emote = false
		end
		Menu.renderGUI()
	end
end)